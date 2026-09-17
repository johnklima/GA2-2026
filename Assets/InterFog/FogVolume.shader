Shader "GitAmend/FogVolume" {
    Properties {
        _DensityTex ("Density", 2D) = "white" {}
        _FogColor ("Fog Color", Color) = (0.85, 0.9, 1.0, 1.0)
        _ShadowColor ("Shadow Color", Color) = (0.45, 0.5, 0.62, 1.0)
        _DensityScale ("Density Scale", Range(0, 20)) = 6.0
    }
    SubShader {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "RenderPipeline" = "UniversalPipeline" }
        Blend One OneMinusSrcAlpha
        ZWrite Off
        Cull Front
        
        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            TEXTURE2D(_DensityTex);
            SAMPLER(sampler_DensityTex);
            float4 _FogColor, _ShadowColor;
            float _DensityScale;
            
            struct Attributes { float4 positionOS : POSITION; };
            
            struct Varyings {
                float4 positionCS : SV_POSITION;
                float3 positionOS : TEXCOORD0;
                float3 cameraOS : TEXCOORD1;
            };
            
            Varyings vert(Attributes input) {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.positionOS = input.positionOS.xyz;
                output.cameraOS = TransformWorldToObject(_WorldSpaceCameraPos);
                return output;
            }
            
            // Slab intersection against the unit cube in object space; returns (tNear, tFar)
            float2 BoxIntersect(float3 ro, float3 rd) {
                float3 t0 = (-0.5 - ro) / rd;
                float3 t1 = (0.5 - ro) / rd;
                float3 tmin = min(t0, t1), tmax = max(t0, t1);
                return float2(max(max(tmin.x, tmin.y), tmin.z), min(min(tmax.x, tmax.y), tmax.z));
            }
            
            half4 frag(Varyings input) : SV_Target { 
                float3 ro = input.cameraOS;
                float3 rd = normalize(input.positionOS - ro);
                float2 t = BoxIntersect(ro, rd);
                t.x = max(t.x, 0.0); // camera may be inside the volume
                
                const int stepCount = 16;
                float stepSize = (t.y - t.x) / stepCount;
                float3 p = ro + rd * (t.x + stepSize * 0.5);
                float3 dp = rd * stepSize;
                
                float3 color = 0.0;
                float transmittance = 1.0;
                
                [unroll]
                for (int i = 0; i < stepCount; i++) {
                    float d = SAMPLE_TEXTURE2D_LOD(_DensityTex, sampler_DensityTex, p.xz + 0.5, 0).r;
                    d *= smoothstep(0.5, -0.3, p.y) * _DensityScale; // fog thins toward the top of the slab
                    float a = 1.0 - exp(-d * stepSize);              // Beer-Lambert per step
                    color += lerp(_ShadowColor.rgb, _FogColor.rgb, saturate(p.y + 0.7)) * (a * transmittance);
                    transmittance *= 1.0 - a;
                    p += dp;
                }
                
                return half4(color, 1.0 - transmittance);
            }
            ENDHLSL
        }
    }
}