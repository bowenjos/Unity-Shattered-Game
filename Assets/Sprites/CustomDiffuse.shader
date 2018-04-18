// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Diffuse (Light)"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}

		Subshader
	{
		Pass
	{
		Tags
	{
		"Queue" = "Opaque"
		"IgnoreProjector" = "True"
		"RenderType" = "Opaque"
		"PreviewType" = "Plane"
	}

		Blend One One
		BlendOp Max
		Lighting Off
		ZWrite Off

		CGPROGRAM

#pragma vertex vert
#pragma fragment frag

		uniform sampler2D _MainTex;

	struct VertexIn
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		float4 col : COLOR;
	};

	struct VertexOut
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float4 col : COLOR;
	};

	VertexOut vert(VertexIn v)
	{
		VertexOut o;

		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		o.col = v.col;

		return o;
	}

	float4 frag(VertexOut i) : COLOR
	{
		return tex2D(_MainTex, i.uv);
	}

		ENDCG
	}
	}
}