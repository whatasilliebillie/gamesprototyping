Shader "Custom/BlurredObject_URP"
{
    Properties
    {
        _BlurSize ("Blur Size", Range(0, 50)) = 3
        _BlurStrength ("Blur Iterations (higher = smoother, more expensive)", Range(1, 4)) = 2
        _Tint ("Tint", Color) = (1,1,1,1)
    }

    SubShader
    {
        // Render after opaques so _CameraOpaqueTexture is populated
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Name "BlurPass"
            Tags { "LightMode"="UniversalForward" }

            ZWrite Off
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Requires "Opaque Texture" enabled in your URP Asset
            TEXTURE2D_X(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);

            CBUFFER_START(UnityPerMaterial)
                float _BlurSize;
                float _BlurStrength;
                float4 _Tint;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 screenPos   : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionHCS = vertexInput.positionCS;
                OUT.screenPos = ComputeScreenPos(vertexInput.positionCS);
                return OUT;
            }

            // Simple radial Gaussian-ish blur sampled around the screen UV
            half4 SampleBlurred(float2 uv, float2 texelSize, float size)
            {
                half4 col = 0;
                float total = 0;

                // 12-tap ring pattern, cheap but decent looking
                const int SAMPLES = 12;
                for (int i = 0; i < SAMPLES; i++)
                {
                    float angle = (i / (float)SAMPLES) * TWO_PI;
                    float2 offset = float2(cos(angle), sin(angle)) * texelSize * size;
                    col += SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, uv + offset);
                    total += 1;
                }
                // Include center sample
                col += SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, uv);
                total += 1;

                return col / total;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.screenPos.xy / IN.screenPos.w;
                float2 texelSize = 1.0 / float2(_ScreenParams.x, _ScreenParams.y);

                half4 col = 0;
                float iterations = max(1, _BlurStrength);

                // Multiple passes at increasing radius = smoother, more convincing blur
                for (int i = 1; i <= 4; i++)
                {
                    if (i > iterations) break;
                    col += SampleBlurred(uv, texelSize, _BlurSize * i);
                }
                col /= iterations;

                col *= _Tint;
                return col;
            }
            ENDHLSL
        }
    }

    FallBack Off
}
