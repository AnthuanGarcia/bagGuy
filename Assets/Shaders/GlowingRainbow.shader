Shader "MyShaders/GlowingRainbow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Tags
        {
            "Queue" = "Background"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float strength = 0.4;
                float t = _Time.y/3.0;
                
                float3 col = float3(0.0, 0.0, 0.0);
                float2 fC = i.vertex;

                #ifdef AA
                for(int i = -1; i <= 1; i++) {
                    for(int j = -1; j <= 1; j++) {

                        fC += float2(i,j)/3.0;
                        
                        #endif
                        
                        //Normalized pixel coordinates (from 0 to 1)
                        float2 pos = fC/_ScreenParams.xy;

                        pos.y /= _ScreenParams.x/_ScreenParams.y;
                        pos = 4.0*(float2(0.5, 0.5) - pos);

                        for(float k = 1.0; k < 7.0; k+=1.0){ 
                            pos.x += strength * sin(2.0*t+k*1.5 * pos.y)+t*0.5;
                            pos.y += strength * cos(2.0*t+k*1.5 * pos.x);
                        }

                        //Time varying pixel colour
                        col += 0.5 + 0.5*cos(_Time.y+pos.xyx+float3(0,2,4));
                        
                        #ifdef AA
                    }
                }

                col /= 9.0;
                #endif
                
                //Gamma
                col = pow(col, float3(0.4545, 0.4545, 0.4545));
                
                //Fragment colour
                return float4(col,1.0);
            }
            ENDCG
        }
    }
}
