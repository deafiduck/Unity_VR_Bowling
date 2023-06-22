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
using UnityEngine.UI;

/*
 * Main GUI script.
 * 
 * The class controls the GUI elements: slides and logo.
 * In Editor Mode the FlyingCamera scripts controls the
 * camera by means of the keyboard arrow keys and the mouse. In this mode
 * the joysticks are disabled as well as the logo.
 * 
 * When running on the device the camera uses the GameCamera script.
 * In this mode the joysticks are active and they are used to control the camera.
 * The Mali logo is active.
 * Right slider controls the shadows LOD factor.
 * Left slider controls the Z value of light position coodinates. 
 * 
 */

public class MainGUI : MonoBehaviour
{
	// Game objects
	[SerializeField]
	GameObject lightSource = default;

	[SerializeField]
	UnifiedShaderControl unifiedController = default;

	[SerializeField]
	Joystick leftJoystick = default;

	[SerializeField]
	Joystick rightJoystick = default;

	[SerializeField]
	GameObject maliLogo = default;

	[SerializeField]
	GameCamera gameCamera = default;

	[SerializeField]
	FlyingCamera flyingCamera = default;

	[SerializeField]
	Slider _leftSlider = default;

	[SerializeField]
	Slider _rightSlider = default;

	void Start()
	{
		
		// Initialize right slider to the value of shadows LOD factor
		_rightSlider.value = unifiedController.ShadowsLodFactor;
		_rightSlider.onValueChanged.AddListener(RightSliderUpdated);

		// Initialize left slider to the value of light position
		_leftSlider.value = lightSource.transform.position.z;
		_leftSlider.onValueChanged.AddListener(LeftSliderUpdated);

		// GUI elements are active only in the device
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			// Disable left joystick
			leftJoystick.gameObject.SetActive(false);
			// Disable right joystick
			rightJoystick.gameObject.SetActive(false);

			// Disable GameCamera script			
			gameCamera.enabled = false;
			// Enable FlyingCamera script
			flyingCamera.enabled = true;

			// Disable Logo
			maliLogo.SetActive(false);

			_leftSlider.gameObject.SetActive(false);
			_rightSlider.gameObject.SetActive(false);
		}
		else
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			// Disable GameCamera script			
			gameCamera.enabled = true;
			// Enable FlyingCamera script
			flyingCamera.enabled = false;
		}
	}

	private void RightSliderUpdated(float arg0)
	{
		unifiedController.ShadowsLodFactor = arg0;
	}

	private void LeftSliderUpdated(float arg0)
	{
		Vector3 lightPos = lightSource.transform.position;
		Vector3 newLightPos = new Vector3(lightPos.x, lightPos.y, arg0);
		lightSource.transform.position = newLightPos;
	}
}
