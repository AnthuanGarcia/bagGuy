Shader "MyShaders/StaticNoise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
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

            #include "UnityCG.cginc"

            const float e = 2.7182818284590452353602874713527;

            fixed4 noise(fixed2 texcoord)
            {
                float G = e + _Time.y * 15.0;
                fixed2 r = (G * sin(G * texcoord.xy));
                float val = frac(r.x * r.y * (1.0 + texcoord.x));
                return fixed4(val, val, val, val);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return noise((2.0 * i.vertex - _ScreenParams.xy) / min(_ScreenParams.x, _ScreenParams.y));
            }
            ENDCG
        }
    }
}
