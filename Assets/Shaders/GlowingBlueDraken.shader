Shader "MyShaders/GlowingBlueDraken"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            float ltime;

            float noise(float2 p)
            {
                return sin(p.x*10.) * sin(p.y*(3. + sin(ltime/11.))) + .2; 
            }

            float2x2 rotate(float angle)
            {
                return float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
            }


            float fbm(float2 p)
            {
                p *= 1.1;
                float f = 0.;
                float amp = .5;
                for(int i = 0; i < 3; i++) {
                    float2x2 modify = rotate(ltime/50. * float(i*i));
                    f += amp*noise(p);
                    p = mul(modify, p);
                    p *= 2.;
                    amp /= 2.2;
                }
                return f;
            }

            float pattern(float2 p, out float2 q, out float2 r) {
                q = float2( fbm(p + float2(1.0, 1.0)),
                        fbm(mul(rotate(0.1*ltime), p) + float2(3.0, 3.0)));

                r = float2( fbm(mul(rotate(.2), q) + float2(0.0, 0.0)),
                        fbm(q + float2(0.0, 0.0)));
                return fbm(p + 1.*r);
            }

            fixed3 hsv2rgb(fixed3 c)
            {
                fixed4 K = fixed4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 p = i.texcoord;
                ltime = _Time.y;
                
                float ctime = _Time.y + fbm(p/8.)*40.;
                float ftime = frac(ctime/6.);

                ltime = floor(ctime/6.) + (1.-cos(ftime*3.1415)/2.);
                ltime = ltime*6.;

                float2 q;
                float2 r;
                float f = pattern(p, q, r);
                fixed3 col = hsv2rgb(fixed3(q.x/10. + ltime/100. + .4, abs(r.y)*3. + .1, r.x + f));
                float vig = 1. - pow(4.*(p.x - .5)*(p.x - .5), 10.);

                vig *= 1. - pow(4.*(p.y - .5)*(p.y - .5), 10.);
                fixed4 fragColor = fixed4(col*vig,1.);

                return fragColor;
            }
            ENDCG
        }
    }
}
