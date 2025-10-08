Shader "Custom/URP_SimpleOcean_WithFoam"
{
   Properties
    {
        _ShallowColor ("Shallow Color", Color) = (0.2,0.6,0.9,1)
        _DeepColor ("Deep Color", Color) = (0.02,0.04,0.25,1)
        _BottomY ("Bottom Y (world)", Float) = -10.0
        _MaxDepth ("Max Depth (meters)", Float) = 15.0

        _FoamTex ("Foam Texture (grayscale)", 2D) = "white" {}
        _FoamTiling ("Foam Tiling", Float) = 0.1
        _FoamScroll ("Foam Scroll XY", Vector) = (0.02, 0.01, 0, 0)
        _FoamThreshold ("Foam Depth Threshold (0..1)", Range(0,1)) = 0.25
        _FoamWidth ("Foam Width", Range(0.0,0.5)) = 0.08
        _FoamStrength ("Foam Strength", Range(0,1)) = 0.9
        _FoamFalloff ("Foam Falloff by depth", Range(0,4)) = 1.5

        _WaveNormalTex ("Optional Wave Normal Map", 2D) = "bump" {}
        _NormalStrength ("Normal Strength", Float) = 0.5

        _Transparency ("Transparency (alpha)", Range(0,1)) = 0.9
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // If include path fails depending on Unity version, you can replace
            // with: #include "UnityCG.cginc" and adjust functions accordingly.

            // Properties (matched)
            float4 _ShallowColor;
            float4 _DeepColor;
            float _BottomY;
            float _MaxDepth;

            TEXTURE2D(_FoamTex);
            SAMPLER(sampler_FoamTex);
            float _FoamTiling;
            float4 _FoamScroll;
            float _FoamThreshold;
            float _FoamWidth;
            float _FoamStrength;
            float _FoamFalloff;

            TEXTURE2D(_WaveNormalTex);
            SAMPLER(sampler_WaveNormalTex);
            float _NormalStrength;

            float _Transparency;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float2 uvFoam : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
            };

            Varyings vert (Attributes v)
            {
                Varyings o;
                float4 worldPos4 = mul(unity_ObjectToWorld, v.positionOS);
                o.worldPos = worldPos4.xyz;

                // Unity function to get clip space position
                o.positionCS = TransformWorldToHClip(o.worldPos);

                // Foam UVs built from world XZ so foam is stable in world space
                o.uvFoam = o.worldPos.xz * _FoamTiling;

                // normal in world space (for simple lighting / normalmap)
                float3 normalWS = normalize(mul((float3x3)unity_ObjectToWorld, v.normalOS));
                o.normalWS = normalWS;

                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                // 1) Compute water depth (simple flat bottom model)
                float waterDepth = i.worldPos.y - _BottomY; // meters
                // clamp and normalize to [0,1]
                float t = saturate(waterDepth / max(0.0001, _MaxDepth));

                // 2) Base color by depth
                float3 baseColor = lerp(_ShallowColor.rgb, _DeepColor.rgb, t);

                // 3) Optional normal perturbation (simple normal map)
                float3 normal = i.normalWS;
                #if defined(_WaveNormalTex)
                // sample normal map (if provided) - convert from [0,1] to [-1,1]
                float2 uv = i.uvFoam + _FoamScroll.xy * _Time.y;
                float4 nSample = SAMPLE_TEXTURE2D(_WaveNormalTex, sampler_WaveNormalTex, uv);
                float3 nMap = normalize((nSample.xyz * 2.0) - 1.0);
                normal = normalize(lerp(normal, nMap, _NormalStrength));
                #endif

                // 4) Simple fresnel for edge brightening (optional nice touch)
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float fresnel = pow(1.0 - saturate(dot(viewDir, normal)), 3.0);
                baseColor += 0.2 * fresnel * (1.0 - t); // brighten shallow edges slightly

                // 5) Foam mask
                // foam texture (grayscale) sampled with scrolling
                float2 foamUV = i.uvFoam + _FoamScroll.xy * _Time.y;
                float foamSample = SAMPLE_TEXTURE2D(_FoamTex, sampler_FoamTex, foamUV).r;

                // foamDepthFactor: shallow areas -> bigger value, deep -> smaller
                float foamDepthFactor = pow(1.0 - t, _FoamFalloff); // [0..1], strong near shore

                // combine texture and depth to produce final mask
                float maskBase = foamSample * foamDepthFactor;

                // thresholding to create lines / edges: use smoothstep for soft edges
                float low = _FoamThreshold - _FoamWidth;
                float high = _FoamThreshold + _FoamWidth;
                float foamMask = smoothstep(low, high, maskBase);

                // control final strength
                foamMask *= _FoamStrength;

                // 6) Mix foam (white) into base color
                float3 finalColor = lerp(baseColor, float3(1.0,1.0,1.0), foamMask);

                // 7) Final alpha
                float alpha = _Transparency;

                return float4(finalColor, alpha);
            }

            ENDHLSL
        } // Pass
    } // SubShader

    FallBack "Unlit/Transparent"
}
