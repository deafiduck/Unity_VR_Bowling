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
using UnityEngine.EventSystems;

/*
 * Joystick script.
 * 
 * This script implements a joystick.
 * 
 */

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	[SerializeField]
	float handleRange = 1;

	[SerializeField]
	float deadZone = 0;

	[SerializeField]
	Canvas canvas = default;

	[SerializeField]
	RectTransform background = default;

	[SerializeField]
	RectTransform handle = default;

	public EventHandler<EventArgs> InputStarted;
	public EventHandler<JoystickEventArgs> InputUpdated;
	public EventHandler<EventArgs> InputEnded;

	private Camera cam = default;
	private Vector2 input = default;

	private void Start()
	{
		Vector2 center = new Vector2(0.5f, 0.5f);
		background.pivot = center;
		handle.anchorMin = center;
		handle.anchorMax = center;
		handle.pivot = center;
		handle.anchoredPosition = Vector2.zero;
	}

	public void SendInputStartedEvent()
	{
		InputStarted?.Invoke(this, EventArgs.Empty);
	}

	public void SendInputUpdatedEvet(Vector2 input)
	{
		InputUpdated?.Invoke(this, new JoystickEventArgs() { Input = input });
	}

	public void SendInputEndedEvent()
	{
		InputEnded?.Invoke(this, EventArgs.Empty);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnDrag(eventData);
		SendInputStartedEvent();
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Passing in a null camera here makes joy sticks work in ScreenSpace - Overlay mode.
		Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
		Vector2 radius = background.sizeDelta / 2;

		input = (eventData.position - position) / (radius * canvas.scaleFactor);
		HandleInput(input.magnitude, input.normalized);
		handle.anchoredPosition = input * radius * handleRange;
		SendInputUpdatedEvet(input);
	}

	private void HandleInput(float magnitude, Vector2 normalised)
	{
		if (magnitude > deadZone)
		{
			if (magnitude > 1)
				input = normalised;
		}
		else
		{
			input = Vector2.zero;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		input = Vector2.zero;
		handle.anchoredPosition = Vector2.zero;
		SendInputEndedEvent();
	}
}

public class JoystickEventArgs : EventArgs {
	public Vector2 Input;
}

