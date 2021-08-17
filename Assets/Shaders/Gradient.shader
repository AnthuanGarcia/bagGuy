Shader "MyShaders/Gradient"
{
    Properties
    {
        _MainTex ("Tint Color (RGB)", 2D) = "white" {}
        _Color0("First Color", Color) = (1,1,1,1)
        _Color1("Second Color", Color) = (1,1,1,1)
    }
    Category
    {
        Tags
        { 
            "Queue" = "Geometry"
            "RenderType"="Opaque"
        }

        SubShader
        {
            // No culling or depth
            Cull Off ZWrite Off ZTest Always

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };

                struct v2f {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                    float2 uvbump : TEXCOORD1;
                    float2 uvmain : TEXCOORD2;
                };

                #define SRGB_TO_LINEAR(c) pow((c), fixed3(2.2, 2.2, 2.2))
                #define LINEAR_TO_SRGB(c) pow((c), fixed3(1.0 / 2.2, 1.0/2.2, 1.0/2.2))
                #define SRGB(r, g, b) SRGB_TO_LINEAR(fixed3(float(r), float(g), float(b)) / 255.0)

                float4 _MainTex_ST;

                sampler2D _MainTex;
                fixed4 _Color0;
                fixed4 _Color1;

                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    o.uvmain = TRANSFORM_TEX( v.texcoord, _MainTex );
                    return o;
                }

                // Gradient noise from Jorge Jimenez's presentation:
                // http://www.iryoku.com/next-generation-post-processing-in-call-of-duty-advanced-warfare
                float gradientNoise(in fixed2 uv)
                {
                    const fixed3 magic = fixed3(0.06711056, 0.00583715, 52.9829189);
                    return frac(magic.z * frac(dot(uv, magic.xy)));
                }

                half4 frag( v2f i ) : COLOR
                {
                    fixed2 a; // First gradient point.
                    fixed2 b; // Second gradient point.

                    a = 0.1 * _ScreenParams.xy;
                    b = _ScreenParams.xy;

                    // Calculate interpolation factor with vector projection.
                    fixed2 ba = b - a;
                    float t = dot(i.vertex - a, ba) / dot(ba, ba);
                    // Saturate and apply smoothstep to the factor.
                    t = smoothstep(0.0, 1.0, clamp(t, 0.0, 1.0));
                    // Interpolate.
                    fixed3 color = lerp(_Color0, _Color1, t);

                    // Convert color from linear to sRGB color space (=gamma encode).
                    color = LINEAR_TO_SRGB(color);

                    // Add gradient noise to reduce banding.
                    color += (1.0/255.0) * gradientNoise(i.vertex) - (0.5/255.0);

                    half4 fragColor = half4(color, 1.0);

                    return fragColor;
                }
                ENDCG
            }
        }
    }
}
