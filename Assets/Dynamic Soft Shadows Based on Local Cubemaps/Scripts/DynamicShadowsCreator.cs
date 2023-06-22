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

/*
 * Dynamic Shadows Creator script
 * 
 * This script places the camera at the light position
 * and orients it to the center of the chess board (shadowCamTarget).
 * The shadow camera only renders the shadows from dynamic objects, i.e.
 * the chess pieces.
 * The dynamic objects must be in the layer DynObjects.
 * When rendering the shadows the camera uses a replacement shader (shadowMapShader)
 * for all chess pieces when no-URP is used.
 * 
 */

[ExecuteInEditMode]
public class DynamicShadowsCreator : MonoBehaviour
{	
	[SerializeField]
	Camera shadowCam = null;

	[SerializeField]
	RenderTexture shadowTexture = null;

	[SerializeField]
	Shader builtInRenderPipelineShadowMapShader = null;

	[SerializeField]
	GameObject shadowCamTarget = null;

	[SerializeField]
	bool isURP = false;

	private void Start()
	{
		if (!isURP)
		{
			shadowCam.SetReplacementShader(builtInRenderPipelineShadowMapShader, "");
		}
	}

	void LateUpdate()
	{
		UpdateCameraTransform();
		shadowCam.SetReplacementShader(builtInRenderPipelineShadowMapShader, "");
	}	
	
	private void UpdateCameraTransform()
	{
		shadowCam.transform.LookAt(shadowCamTarget.transform.position);
	}
	
	
	public DynamicShadowsData GetShadowData()
	{
		DynamicShadowsData data;
		data.shadowTexture = shadowTexture;
		data.viewProjMatrix = shadowCam.projectionMatrix * shadowCam.worldToCameraMatrix;
		return data;
	}

	public struct DynamicShadowsData
	{
		public RenderTexture shadowTexture;
		public Matrix4x4 viewProjMatrix;
	}

}

