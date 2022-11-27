// Exerpts from COMP30019 workshops and Unity3d manual
Shader "WaterShader"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #include "AutoLight.cginc"

            sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct vertOut
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1)
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
            };
            vertOut vert (appdata_base v)
            {
                v.vertex += float4(0.0f, sin(0.2*v.vertex.x + _Time.y) * 0.5f, 0.0f, 0.0f);
                vertOut o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal,1));
                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag (vertOut o) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, o.uv);
                fixed shadow = SHADOW_ATTENUATION(o);
                fixed3 lighting = o.diff * shadow + o.ambient;
                col.rgb *= lighting;
                return col;
            }
            ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}