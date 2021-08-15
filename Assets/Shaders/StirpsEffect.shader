Shader "MyShaders/StirpsEffect"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag // we've changed the name of the func to "frag". The implementation can be found below
            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;

            fixed3 pointo(float2 p) {
                return length(p)<0.005 ? fixed3(1.0, 1.0, 1.0) : fixed3(0.0, 0.0, 0.0);
            }

            float4 frag(v2f_img i) : COLOR {
            
                // Normalized pixel coordinates (from 0 to 1)
                float2 uv = i.uv;
                
                // LINE ax+by+c = 0
                // y = -(ax+c)/b
                // through two points
                // a = y2-y1 
                // b = x1-x2
                // c = x2y1-x1y2
                
                float2 p1 = float2(
                    0.5+sin(_Time.y*0.5)*0.3,
                    0.5+cos(_Time.y*0.5)*0.3);
                float2 p2 = float2(
                    0.5+sin(_Time.y*0.2)*0.2,
                    0.5+cos(_Time.y*0.2)*0.2);
                
                float a = p2.y-p1.y;
                float b = p1.x-p2.x;
                float c = p2.x*p1.y-p1.x*p2.y;
            
                float x = uv.x;
                float y = -(a*x+c)/b;
                
                float3 col;

                if (uv.y > y) {
                    col = tex2D(_MainTex, uv).rgb;
                } else {
                    // closest point to line
                    // https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
                    float x0 = uv.x;
                    float y0 = uv.y;
                    float2 uv2 = float2(
                        b*( b*x0-a*y0) - a*c,
                        a*(-b*x0+a*y0) - b*c
                    );
                    uv2 /= a*a+b*b;
                    col = tex2D(_MainTex, uv2 ).rgb;
                }

                col+= pointo(p1)+pointo(p2);
                return float4(col,1.0);
            }
            ENDCG
        }
    }
}
