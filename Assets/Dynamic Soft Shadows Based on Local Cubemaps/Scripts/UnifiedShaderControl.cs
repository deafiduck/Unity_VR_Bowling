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
 * Unified Shader Control script
 * 
 * This class controls the values exposed by shaders to the users.
 * 
 */

[ExecuteInEditMode]
public class UnifiedShaderControl : MonoBehaviour
{
	[SerializeField]
	Material[] sceneMaterials = default;

	[SerializeField]
	Color ambientColor = default;

	[SerializeField]
	[Range(0.0f, 5.0f)]
	float shadowsLodFactor = default;

	[SerializeField]
	bool renderingCubemap = default;

	[SerializeField]
	[Range(1.0f, 20.0f)]
	float lightShadowsContrast = default;

	[SerializeField]
	Color shadowTint = default;

	public float ShadowsLodFactor { get => shadowsLodFactor; set => shadowsLodFactor = value; }

	void Update()
	{
		SetShaderData();
	}

	void SetShaderData()
	{
		for (int i = 0; i < sceneMaterials.Length; i++)
		{
			sceneMaterials[i].SetVector("_AmbientColor", ambientColor);
			sceneMaterials[i].SetFloat("_ShadowLodFactor", shadowsLodFactor);
			sceneMaterials[i].SetFloat("_LightToShadowsContrast", lightShadowsContrast);
			sceneMaterials[i].SetVector("_ShadowsTint", shadowTint);

			// Enabling/Disabling the keywords
			if (renderingCubemap)
			{
				sceneMaterials[i].EnableKeyword("CUBEMAP_RENDERING_ON");
				sceneMaterials[i].DisableKeyword("CUBEMAP_RENDERING_OFF");
			}
			else
			{
				sceneMaterials[i].EnableKeyword("CUBEMAP_RENDERING_OFF");
				sceneMaterials[i].DisableKeyword("CUBEMAP_RENDERING_ON");
			}
		}
	}
}


