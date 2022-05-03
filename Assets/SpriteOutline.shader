// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Outline"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		// Add values to determine if outlining is enabled and outline color.
		[PerRendererData] _Outline("Outline", Float) = 0
		[PerRendererData] _OutlineColor("Outline Color", Color) = (1,1,1,1)
		[PerRendererData] _OutlineSize("Outline Size", int) = 1
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma shader_feature ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			float _Outline;
			fixed4 _OutlineColor;
			int _OutlineSize;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float4 _MainTex_TexelSize;

			fixed4 SampleSpriteTexture(float2 uv)
			{
				fixed4 color = tex2D(_MainTex, uv);

				#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				color.a = tex2D(_AlphaTex, uv).r;
				#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}

			float a(sampler2D tex, float2 coord)
			{
				return tex2D(tex, coord).a * (coord.x >= 0) * (coord.x <= 1) * (coord.y >= 0) * (coord.y <= 1) ;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;

				// If outline is enabled and there is a pixel, try to draw an outline.
				if (_Outline > 0 && _OutlineSize > 0 && c.a != 0) {
					float totalAlpha = 
						a(_MainTex, IN.texcoord + fixed2(0, _MainTex_TexelSize.y)) *
						a(_MainTex, IN.texcoord - fixed2(0, _MainTex_TexelSize.y)) *
						a(_MainTex, IN.texcoord + fixed2(_MainTex_TexelSize.x, 0)) *
						a(_MainTex, IN.texcoord - fixed2(_MainTex_TexelSize.x, 0)) *
						a(_MainTex, IN.texcoord + fixed2(_MainTex_TexelSize.x, _MainTex_TexelSize.y)) *
						a(_MainTex, IN.texcoord - fixed2(_MainTex_TexelSize.x, _MainTex_TexelSize.y)) *
						a(_MainTex, IN.texcoord + fixed2(_MainTex_TexelSize.x, -_MainTex_TexelSize.y)) *
						a(_MainTex, IN.texcoord - fixed2(_MainTex_TexelSize.x, -_MainTex_TexelSize.y));

					if (totalAlpha == 0) {
						c.rgb = lerp(c.rgb, _OutlineColor, _OutlineColor.a);
					}
				}

				c.rgb *= c.a;

				return c;
			}
			ENDCG
		}
	}
}