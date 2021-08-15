Shader "MyShaders/Glowing70s"
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
            #pragma vertex SpriteVert
            #pragma fragment frag // we've changed the name of the func to "frag". The implementation can be found below
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            #define RADIANS 0.017453292519943295

            const float zoom = 40.0;
            const float brightness = 0.975;
            float fScale = 1.25;

            float cosRange(float degrees, float range, float minimum) {
                return (((1.0 + cos(degrees * RADIANS)) * 0.5) * range) + minimum;
            }

            fixed4 frag (v2f i) : SV_Target {
                
                float time = _Time.y * 1.25;
                fixed2 uv = i.vertex.xy / _ScreenParams.xy;
                fixed2 p  = (2.0 * i.vertex.xy - _ScreenParams.xy) / max(_ScreenParams.x, _ScreenParams.y);
                float ct = cosRange(time*5.0, 3.0, 1.1);
                float xBoost = cosRange(time*0.2, 5.0, 5.0);
                float yBoost = cosRange(time*0.1, 10.0, 5.0);
                
                fScale = cosRange(time * 15.5, 1.25, 0.5);
                
                for(float j=1.0; j<zoom; j++) {
                    fixed2 newp=p;
                    newp.x+=0.25/j*sin(j*p.y+time*cos(ct)*0.5/20.0+0.005*j)*fScale+xBoost;		
                    newp.y+=0.25/j*sin(j*p.x+time*ct*0.3/40.0+0.03*(j+15.0))*fScale+yBoost;
                    p=newp;
                }
                
                fixed3 col = fixed3(0.5*sin(3.0*p.x)+0.5, 0.5*sin(3.0*p.y)+0.5, sin(p.x+p.y));
                //col = mul(col, brightness);
                
                // Add border
                float vigAmt = 5.0;
                float vignette = (1.0-vigAmt*(uv.y-0.5)*(uv.y-0.5))*(1.0-vigAmt*(uv.x-0.5)*(uv.x-0.5));
                float extrusion = (col.x + col.y + col.z) / 4.0;
                extrusion *= 1.5;
                extrusion *= vignette;
                
                fixed4 fragColor = fixed4(col, extrusion);

                return fragColor;
            }
            ENDCG
        }
    }
}
