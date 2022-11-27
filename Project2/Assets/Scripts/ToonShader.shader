// Guided by roystan.net toonshader tutorial https://roystan.net/articles/toon-shader.html.
Shader "ToonShader"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		_Attenuation("Attenuation", float) = 1
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
	}
	SubShader
	{
		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct vertIn
			{
				float4 vertex : POSITION;		
				float3 normal : NORMAL;		
				float4 uv : TEXCOORD0;
				
			};

			struct vertOut
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			vertOut vert (vertIn v)
			{
				vertOut o;
				// transforming vertex and normal from object space to world space
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);

				// Transform vertex in world coordinates to camera coordinates, and pass UV
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				TRANSFER_SHADOW(o)

				return o;
			}
			
			float4 _Color;

			float4 _AmbientColor;

			float _Attenuation;

			// takes the output of the vertex shader
			float4 frag (vertOut o) : SV_Target
			{
				float4 sample = tex2D(_MainTex, o.uv);

				float shadow = SHADOW_ATTENUATION(o);

				float3 lightDirection = normalize(_WorldSpaceLightPos0);
				float3 normal = normalize(o.worldNormal);
				float LdotN = dot(lightDirection, normal);

				// if the light isn't hitting directly, then full dark
				float lightIntensity = LdotN * shadow > 0 ? 1 : 0;
				float4 light = lightIntensity * _LightColor0;

				// Diffuse
				float Kd = 1;
				float3 dif = _Attenuation * Kd * _Color * saturate(LdotN);

				float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
				returnColor.rgb = _Color * sample * (_AmbientColor + dif.rgb + light.rgb);
				returnColor.a = _Color.a * sample.a * (_AmbientColor.a + light.a);
				return returnColor;
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}