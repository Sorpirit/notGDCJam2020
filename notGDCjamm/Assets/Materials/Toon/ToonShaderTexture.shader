Shader "Custom/ToonShaderTexture"
{
	Properties{
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0, 0.1)) = .005
		_MainTex("Base (RGB)", 2D) = "white" { }
		_Color("Main Color", Color) = (1,1,1,1) //gradient colors
		_ShadowColor("Shadow Color", Color) = (0,0,0,0)

		_Brightness("Brightness", Range(0,1)) = 0.3
		_Strength("Strength", Range(0,1)) = 0.5
		_Detail("Detail", Range(0,1)) = 0.3

	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			Pass
			{
			// Pass drawing outline
			Cull Front

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			uniform float _Outline;
			uniform float4 _OutlineColor;
			uniform float4 _MainTex_ST;
			uniform sampler2D _MainTex;


			struct v2f
			{
				float4 pos : POSITION;
				float4 color : COLOR;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 offset = TransformViewToProjection(norm.xy);
				o.pos.xy += offset * _Outline;
				o.color = _OutlineColor;
				return o;
			}

			half4 frag(v2f i) :COLOR
			{
				return i.color;
			}

			ENDCG
		}

		Pass
		{
				// pass drawing object

				CGPROGRAM
				#include "UnityCG.cginc"
				#pragma vertex vert
				#pragma fragment frag

				uniform float4 _MainTex_ST;
				uniform sampler2D _MainTex;
				uniform float4 _Color;
				float4 _ShadowColor;
				float _Brightness;
				float _Strength;
				float _Detail;

				struct v2f {
					float4 pos : POSITION;
					float2 uv : TEXCOORD0;
					float4 color : COLOR;
					half3 worldNormal: NORMAL;
				};

				v2f vert(appdata_base v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;
					o.color = _Color;
					o.worldNormal = UnityObjectToWorldNormal(v.normal); //turn normal into world space 
					return o;
				}

				float Toon(float3 normal, float3 lightDir) {
					float NdotL = max(0.0f, dot(normalize(normal), normalize(lightDir))); //max to clamp everything under 0 as one solid shadow
					return floor(NdotL / _Detail); //divide the light
				}


				half4 frag(v2f i) :COLOR
				{
					return _ShadowColor + (i.color *Toon(i.worldNormal, _WorldSpaceLightPos0.xyz)*_Strength*_Color + _Brightness); //works with DirLight
					//*_Strength //how strong are the blocks
					//	*_Strength //tint the light
					//+ _Brightness; //control how dark shadows are ); //+ tex2D(_MainTex, _MainTex_ST.xy * i.uv.xy) + _MainTex_ST.zw 
				}

				ENDCG
			}

				//  Shadow rendering pass
				Pass{
					Name "ShadowCaster"
					Tags { "LightMode" = "ShadowCaster" }

					ZWrite On ZTest LEqual Cull Off

					CGPROGRAM
					#pragma target 2.0

					#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON //_ALPHAPREMULTIPLY_ON
					#pragma skip_variants SHADOWS_SOFT
					#pragma multi_compile_shadowcaster

					#pragma vertex vertShadowCaster
					#pragma fragment fragShadowCaster

					#include "UnityStandardShadow.cginc"

					ENDCG
			}

		}
			FallBack "Diffuse"
}