Shader "MyShaders/Glowing Blue"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
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
            };

            float4 _MainTex_ST;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float rand(fixed2 n) {
                return frac(sin(dot(n, fixed2(12.9898, 4.1414))) * 43758.5453);
            }

            float noise(fixed2 p) {
                fixed2 ip = floor(p);
                fixed2 u = frac(p);
                u = u*u*(3.0-2.0*u);
                float res = lerp(
                    lerp(rand(ip),rand(ip+fixed2(1.0,0.0)),u.x),
                    lerp(rand(ip+fixed2(0.0,1.0)),rand(ip+fixed2(1.0,1.0)),u.x),u.y);
                return res*res;
            }

            const float2x2 m2 = float2x2(0.8,-0.6,0.6,0.8);

            float fbm( in fixed2 p ){
                float f = 0.0;
                f += 0.5000*noise( p ); p = mul(m2, p*2.02);
                f += 0.2500*noise( p ); p = mul(m2, p*2.03);
                f += 0.1250*noise( p ); p = mul(m2, p*2.01);
                f += 0.0625*noise( p );
                return f/0.769;
            }
            
            float pattern( in fixed2 p ) {
                float qAux = fbm(p + fixed2(0.0,0.0));
                fixed2 q = fixed2(qAux, qAux);
                float rAux = fbm( p + 4.0*q + fixed2(1.7,9.2));
                fixed2 r = fixed2(rAux, rAux);
                r+= _Time.y * 0.15;
                return fbm( p + 1.760*r );
            }

            half4 frag( v2f i ) : COLOR {
                //fixed2 uv = i.texcoord;
                fixed2 uv = (2.0 * i.vertex - _ScreenParams.xy) / min(_ScreenParams.x, _ScreenParams.y);
                
                uv *= 4.5; // Scale UV to make it nicer in that big screen !
                float displacement = pattern(uv);
                half4 color = fixed4(displacement * 1.2, 0.2, displacement * 5., 1.);
                
                color.a = min(color.r * 0.25, 1.); // Depth for CineShader

                return color;
            }

            ENDCG
        }
    }
}
