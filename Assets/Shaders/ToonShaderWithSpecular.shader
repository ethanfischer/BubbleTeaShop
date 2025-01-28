Shader "Custom/CelShaderWithSpecular"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Steps ("Shading Steps", Range(1, 8)) = 3
        _ShadowIntensity ("Shadow Intensity", Range(0, 1)) = 0.5
        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
        _Shininess ("Shininess", Range(1, 512)) = 64
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
            float4 _SpecularColor;
            float _Shininess;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Light direction
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // Diffuse lighting
                float NdotL = max(0, dot(i.worldNormal, lightDir));
                float quantized = floor(NdotL * _Steps) / _Steps;
                float shadow = lerp(_ShadowIntensity, 1.0, quantized);

                // Specular highlights
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 reflectDir = reflect(-lightDir, i.worldNormal);

                float specular = pow(max(0, dot(viewDir, reflectDir)), _Shininess);
                half4 specularColor = _SpecularColor * specular;

                // Base color with diffuse and specular
                half4 textureColor = tex2D(_MainTex, float2(0.5, 0.5));
                half4 finalColor = (_Color * textureColor * shadow) + specularColor;

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
