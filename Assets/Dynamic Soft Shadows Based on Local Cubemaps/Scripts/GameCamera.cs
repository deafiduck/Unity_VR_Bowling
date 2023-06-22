/*
 * This confidential and proprietary software may be used only as
 * authorised by a licensing agreement from ARM Limited
 * (C) COPYRIGHT 2016 ARM Limited
 * ALL RIGHTS RESERVED
 * The entire notice above must be reproduced on all authorised
 * copies and copies may only be made to the extent permitted
 * by a licensing agreement from ARM Limited.
 */

using System;
using UnityEngine;

/*
 * Game Camera script.
 * 
 * Class that controls the camera by means of joysticks when running on the device.
 * Right Joystick controls the camera orientation.
 * Left Joystick controls the camera motion.
 * 
 */

public class GameCamera : MonoBehaviour
{
	[SerializeField]
	Joystick leftJoystick = default;
		 
	[SerializeField]
	Joystick rightJoystick = default;

	[SerializeField]
	float speed = 6.0f;

	[SerializeField]
	float rotationSeneitivity = 100f;

	[SerializeField]
	Transform cameraBody = default;

	[SerializeField]
	CharacterController controller = default;

	private float turnSmoothVelocity;
	private Vector3 directionalInput;
	private Vector2 rotationalInput;
	private float xRotation;

	void Start()
	{
		leftJoystick.InputUpdated += HandleDirectionInputUpdated;
		leftJoystick.InputEnded += HandleDirectionInputEnded;
		rightJoystick.InputUpdated += HandleRotationInputUpdated;
		rightJoystick.InputEnded += HandleRotationInputEnded;
	}

	private void HandleDirectionInputUpdated(object sender, JoystickEventArgs e)
	{
		directionalInput = new Vector3(e.Input.x, 0f, e.Input.y).normalized;
	}

	private void HandleDirectionInputEnded(object sender, EventArgs e)
	{
		directionalInput = Vector3.zero;
	}

	private void HandleRotationInputUpdated(object sender, JoystickEventArgs e)
	{
		rotationalInput = e.Input.normalized;
	}

	private void HandleRotationInputEnded(object sender, EventArgs e)
	{
		rotationalInput = Vector2.zero;
	}

	void Update()
	{
		HandleMovement();
		HandleRotation();
	}

	private void HandleRotation()
	{
		if (rotationalInput.magnitude >= 0.1f)
		{
			float rotateX = rotationalInput.x * rotationSeneitivity * Time.deltaTime;
			float rotateY = rotationalInput.y * rotationSeneitivity * Time.deltaTime;

			xRotation -= rotateY;
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);

			transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
			cameraBody.Rotate(Vector3.up * rotateX);
		}
	}

	private void HandleMovement()
	{
		if (directionalInput.magnitude >= 0.1f)
		{
			Vector3 moveDirection = GetMoveDirection();
			moveDirection.y = 0;
			controller.Move(moveDirection.normalized * speed * Time.deltaTime);
		}
	}

	private Vector3 GetMoveDirection()
	{
		//Camera is used to keep the player moving in the direction the camera is pointed when it rotates.
		float targetAngle = Mathf.Atan2(directionalInput.x, directionalInput.z) * Mathf.Rad2Deg + transform.eulerAngles.y;

		//Turn target angle into a direction. 
		Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
		return moveDirection;
	}
}