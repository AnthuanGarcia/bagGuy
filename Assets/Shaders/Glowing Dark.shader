Shader "MyShaders/Glowing Dark"
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

            float4 frag(v2f i) : SV_Target {
                float iTime = _Time * 10.0;
                fixed2 uv = (2.0 * i.vertex - _ScreenParams.xy) / min(_ScreenParams.x, _ScreenParams.y);
                
                for(float i = 1.0; i < 10.0; i++){
                    uv.x += 0.6 / i * cos(i * 2.5* uv.y + iTime);
                    uv.y += 0.6 / i * cos(i * 1.5 * uv.x + iTime);
                }
                
                fixed4 fragColor = fixed4(fixed3(0.1, 0.1, 0.1)/abs(sin(iTime-uv.y-uv.x)),1.0);

                return fragColor;
            }

            ENDCG
        }
    }
}
