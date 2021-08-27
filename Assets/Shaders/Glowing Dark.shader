Shader "MyShaders/Glowing Dark"
{
    Properties
    {
        [PerRendererData] _MainTex ("", 2D) = "white" {}
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
