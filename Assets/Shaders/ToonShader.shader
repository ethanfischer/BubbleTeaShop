Shader "Custom/CelShaderUniformSides"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Steps ("Shading Steps", Range(1, 8)) = 3
        _ShadowIntensity ("Shadow Intensity", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            int _Steps;
            float _ShadowIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                // Snap normals to the nearest axis for uniform sides
                float3 snappedNormal = round(v.normal * 2.0) * 0.5;
                o.worldNormal = UnityObjectToWorldNormal(snappedNormal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Calculate light direction
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // Diffuse lighting
                float NdotL = max(0, dot(i.worldNormal, lightDir));

                // Quantize the lighting to create steps
                float quantized = floor(NdotL * _Steps) / _Steps;

                // Blend shadow intensity with base color
                float shadow = lerp(_ShadowIntensity, 1.0, quantized);

                // Base color with shadow blending
                half4 textureColor = tex2D(_MainTex, float2(0.5, 0.5));
                half4 finalColor = _Color * textureColor * shadow;

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
