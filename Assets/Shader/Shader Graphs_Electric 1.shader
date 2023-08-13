Shader "Shader Graphs/Electric 1" {
	Properties {
		[NoScaleOffset] Texture2D_66c7158934134db39efb7222f5695ece ("Lens", 2D) = "white" {}
		Vector1_c81db5dcacf64207937491849fde8519 ("Strength", Float) = 4
		_Transparency ("Transparency", Range(0, 1)) = 0.5
		_LenColor ("Len Color", Vector) = (0,0,0,0)
		_LenColor_1 ("Blood Color", Vector) = (0,0,0,0)
		[NoScaleOffset] Texture2D_ebd846ce464943c0948e11e2c087faf2 ("Noise", 2D) = "white" {}
		_Blood ("Blood Strength", Range(0, 1)) = 0
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
}