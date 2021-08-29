Shader "MyShaders/StaticNoise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #pragma vertex vert
            #pragma fragment frag // we've changed the name of the func to "frag". The implementation can be found below
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

            const float e = 2.7182818284590452353602874713527;

            fixed4 noise(fixed2 texcoord)
            {
                float G = e + _Time.y * 5.0;
                fixed2 r = (G * sin(G * texcoord.xy));
                float val = frac(r.x * r.y * (1.0 + texcoord.x));
                return fixed4(val, val, val, val);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.vertex / _ScreenParams.xy;
                return noise(uv);
            }
            ENDCG
        }
    }
}
