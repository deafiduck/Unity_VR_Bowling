/*
 * This confidential and proprietary software may be used only as
 * authorised by a licensing agreement from ARM Limited
 * (C) COPYRIGHT 2016 ARM Limited
 * ALL RIGHTS RESERVED
 * The entire notice above must be reproduced on all authorised
 * copies and copies may only be made to the extent permitted
 * by a licensing agreement from ARM Limited.
 */

using UnityEngine;

/**
 * Set Shadows script
 * 
 * This class sends information to the material that receives shadows from dynamic object (chess piece)
 * necessary to project the shadows: view-projection matrix and shadows texture rendered at runtime.
 * Every material that receives shadows from dynamic objects must have this script attached.
 * 
 */
public class SetShadows : MonoBehaviour
{
	[SerializeField]
	DynamicShadowsCreator shadowsCamera = default;

	[SerializeField]
	new Renderer renderer = default;

	void OnWillRenderObject()
	{
		DynamicShadowsCreator.DynamicShadowsData shadowsData = shadowsCamera.GetShadowData();

		int numMats = renderer.sharedMaterials.Length;

		for (int i = 0; i < numMats; i++)
		{
			renderer.sharedMaterials[i].SetMatrix("_ShadowsViewProjMat", shadowsData.viewProjMatrix);

		}
	}
}
