Shader "MyShaders/DuplicatedAbstract"
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
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;

            float noise( in fixed3 x )
            {
                fixed3 p = floor(x);
                fixed3 f = frac(x);
                f = f*f*(3.0-2.0*f);
                fixed2 uv = (p.xy+fixed2(37.0,17.0)*p.z) + f.xy;
                fixed2 val = (uv+ 0.5)/256.0;
                fixed2 rg = tex2Dlod( _MainTex, float4(val.x, val.y, 0.0, 0.0)).yx;

                return 0. - 0.5*lerp( rg.x, rg.y, f.z );
            }

            fixed4 frag (v2f_img i) : COLOR
            {
                fixed2 p =  i.uv;
                fixed4 b = tex2D(_MainTex, p);
            
                for (float j = 0.; j < 1.; j+=.2)
                {
                    float valT = j + _Time.y;
                    fixed2 aux = p * j + noise(fixed3(valT, valT, valT))/1.1;
                    fixed4 d = tex2D(_MainTex, fixed4(aux.x, aux.y, 0.0, 0.0));
                    b += pow(d, fixed4(1., 1., 1., 1.));
                }

                fixed4 c = b*.25;

                return c;
            }
            ENDCG
        }
    }
}
