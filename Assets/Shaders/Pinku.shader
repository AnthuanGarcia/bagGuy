Shader "MyShaders/GlamPink"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        
        Tags
        {
            "Queue" = "Background"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment frag // we've changed the name of the func to "frag". The implementation can be found below
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            /*#pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord: TEXCOORD0;
            };

            struct v2f {
                float4 vertex : POSITION;
            };

            float4 _MainTex_ST;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }*/

            float colormap_red(float x) {
                if (x < 0.0) {
                    return 54.0 / 255.0;
                } else if (x < 20049.0 / 82979.0) {
                    return (829.79 * x + 54.51) / 255.0;
                } else {
                    return 1.0;
                }
            }

            float colormap_green(float x) {
                if (x < 20049.0 / 82979.0) {
                    return 0.0;
                } else if (x < 327013.0 / 810990.0) {
                    return (8546482679670.0 / 10875673217.0 * x - 2064961390770.0 / 10875673217.0) / 255.0;
                } else if (x <= 1.0) {
                    return (103806720.0 / 483977.0 * x + 19607415.0 / 483977.0) / 255.0;
                } else {
                    return 1.0;
                }
            }

            float colormap_blue(float x) {
                if (x < 0.0) {
                    return 54.0 / 255.0;
                } else if (x < 7249.0 / 82979.0) {
                    return (829.79 * x + 54.51) / 255.0;
                } else if (x < 20049.0 / 82979.0) {
                    return 127.0 / 255.0;
                } else if (x < 327013.0 / 810990.0) {
                    return (792.02249341361393720147485376583 * x - 64.364790735602331034989206222672) / 255.0;
                } else {
                    return 1.0;
                }
            }

            fixed4 colormap(float x) {
                return fixed4(colormap_red(x), colormap_green(x), colormap_blue(x), 1.0);
            }


            float rand(fixed2 n) { 
                return frac(sin(dot(n, fixed2(12.9898, 4.1414))) * 43758.5453);
            }

            float noise(fixed2 p){
                fixed2 ip = floor(p);
                fixed2 u = frac(p);
                u = u*u*(3.0-2.0*u);

                float res = lerp(
                    lerp(rand(ip),rand(ip+fixed2(1.0,0.0)),u.x),
                    lerp(rand(ip+fixed2(0.0,1.0)),rand(ip+fixed2(1.0,1.0)),u.x),u.y);
                return res*res;
            }

            static const float2x2 mtx = float2x2( 0.80,  0.60, -0.60,  0.80 );

            float fbm( fixed2 p )
            {
                float f = 0.0;
                //float iTime = _Time * 10.0;

                f += 0.500000*noise( p + _Time.y  ); p = mul(mtx, p * 2.02);
                f += 0.031250*noise( p ); p = mul(mtx, p*2.01);
                f += 0.250000*noise( p ); p = mul(mtx, p*2.03);
                f += 0.125000*noise( p ); p = mul(mtx, p*2.01);
                f += 0.062500*noise( p ); p = mul(mtx, p*2.04);
                f += 0.015625*noise( p + sin(_Time.y) );

                return f/0.96875;
            }

            float pattern( in fixed2 p )
            {
                return fbm( p + fbm( p + fbm( p ) ) );
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed2 uv = i.texcoord;
                fixed2 uv = (2.0 * i.vertex - _ScreenParams.xy) / min(_ScreenParams.x, _ScreenParams.y);
                float shade = pattern(uv);
                fixed4 fragColor = fixed4(colormap(shade).rgb, shade);
                
                return fragColor;
            }
            ENDCG
        }
    }
}
