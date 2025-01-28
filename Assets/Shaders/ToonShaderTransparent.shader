Shader "Custom/TransparentToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 0.5) // Color with alpha for transparency
        _Steps ("Shading Steps", Range(1, 8)) = 3
        _ShadowIntensity ("Shadow Intensity", Range(0, 1)) = 0.5
        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
        _Shininess ("Shininess", Range(1, 512)) = 64
        _FresnelPower ("Fresnel Power", Range(0, 5)) = 1.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
                float3 viewDir : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            int _Steps;
            float _ShadowIntensity;
            float4 _SpecularColor;
            float _Shininess;
            float _FresnelPower;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // Calculate view direction
                o.viewDir = normalize(_WorldSpaceCameraPos - o.worldPos);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Light direction
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // Diffuse lighting (cel-shaded)
                float NdotL = max(0, dot(i.worldNormal, lightDir));
                float quantized = floor(NdotL * _Steps) / _Steps;
                float shadow = lerp(_ShadowIntensity, 1.0, quantized);

                // Specular highlights
                float3 reflectDir = reflect(-lightDir, i.worldNormal);
                float specular = pow(max(0, dot(i.viewDir, reflectDir)), _Shininess);
                half4 specularColor = _SpecularColor * specular;

                // Fresnel effect for glass edge glow
                float fresnel = pow(1.0 - max(0, dot(i.viewDir, i.worldNormal)), _FresnelPower);

                // Base color with diffuse, specular, and transparency
                half4 textureColor = tex2D(_MainTex, float2(0.5, 0.5));
                half4 finalColor = (_Color * textureColor * shadow) + specularColor;

                // Apply Fresnel for edge effect
                finalColor.rgb += fresnel * _SpecularColor.rgb;

                // Maintain transparency
                finalColor.a = _Color.a;
                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
