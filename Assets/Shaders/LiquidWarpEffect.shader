Shader "MyShaders/LiquidWarpEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    Category 
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque" }

        SubShader
        {

            GrabPass {                         
                Tags { "LightMode" = "Always" }
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

                struct appdata{
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                float4 _MainTex_ST;
                sampler2D _GrabTexture;

                fixed4 frag (v2f i) : SV_Target
                {
                    //i.uvgrab /= _ScreenParams;
                    //float4 uv = i.vertex / _ScreenParams;

                    float waveStrength = 0.02;
                    float frequency = 30.0;
                    float waveSpeed = 5.0;
                    fixed4 sunlightColor = fixed4(1.0,0.91,0.75, 1.0);
                    float sunlightStrength = 5.0;
                    //
                    
                    //fixed4 tapPoint = fixed4(0.0, 0.0, 0.0, 0.0);
                    fixed2 uv = i.vertex / _ScreenParams.xy;
                    float modifiedTime = _Time.y * waveSpeed;
                    float aspectRatio = _ScreenParams.x/_ScreenParams.y;
                    fixed2 distVec = uv;
                    distVec.x *= aspectRatio;
                    float distance = length(distVec);
                    fixed2 newTexCoord = uv;
                    
                    float multiplier = (distance < 1.0) ? ((distance-1.0)*(distance-1.0)) : 0.0;
                    float addend = (sin(frequency*distance-modifiedTime)+1.0) * waveStrength * multiplier;
                    newTexCoord += addend;    
                    
                    fixed4 colorToAdd = sunlightColor * sunlightStrength * addend;
                    
                    fixed4 fragColor = tex2Dproj(_GrabTexture, fixed4(newTexCoord.x, newTexCoord.y, 1.0, 1.0));

                    return fragColor;
                }

                ENDCG
            }
        }
    }
}
