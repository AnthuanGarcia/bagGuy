Shader "MyShaders/ASCIISpiral"
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

            //sampler2D _MainTex;

            #define ANIMATE

            #define pi 3.1415926535897932384626433832795
            #define pi2 6.283185307179586

            #define pi4th 0.7853981633974483

            fixed2 size = fixed2(24.0,8.0);

            fixed2 celUVCoords(fixed2 uv) {
                return ((floor(uv/size)*size) / _ScreenParams.xy);
            }

            fixed2 linearCoords(fixed2 uv) {
                return (uv / _ScreenParams.xy);
            }

            fixed3 lerp(float t, fixed3 a, fixed3 b) {
                return t*a + (1.0-t)*b;
            }

            float digit(float x) {
                float n = 0.0;
                if (x >= 0.0) n = 6595878.0;
                if (x >= 1.0) n = 14815366.0;
                if (x >= 2.0) n = 15798566.0;
                if (x >= 3.0) n = 16005391.0;
                if (x >= 4.0) n = 8666409.0;
                if (x >= 5.0) n = 16006190.0;
                if (x >= 6.0) n = 16038959.0;
                if (x >= 7.0) n = 1118479.0;
                if (x >= 8.0) n = 16029999.0;
                if (x >= 9.0) n = 16006447.0;
                return n;
            }

            float mod(float a, float b)
            {
                return a - b * floor(a/b);
            }

            float character(float n, fixed2 p)
            {
                p = floor(p*fixed2(4.0, -4.0) + 2.5);
                if (clamp(p.x, 0.0, 4.0) == p.x && clamp(p.y, 0.0, 4.0) == p.y)
                {
                    if (int(mod(n/exp2(p.x + 5.0*p.y), 2.0)) == 1) return 1.0;
                }	
                return 0.0;
            }

            int _abs(int a) {
                return a < 0 ? -a : a;
            }

            int _max(int a, int b) {
                return a > b ? a : b;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 uv = i.vertex;
                fixed2 box = celUVCoords(uv);

                fixed3 col = tex2D(_MainTex, floor(uv/8.0)*8.0/_ScreenParams.xy).rgb;
                
                int2 pt = int2(box * (_ScreenParams.xy / size));
                pt = int2(13,21) - pt;
                
            #ifdef ANIMATE
                float angleOffset = mod(pi - pi/4.0 + _Time.y, 2.0*pi);
            #else
                float angleOffset =  pi - pi/4.0;
            #endif
                
                float thisAngle = atan2(float(pt.y), float(pt.x)) + angleOffset;
                int ring = _max(_abs(pt.x), _abs(pt.y)) * 2 + 1;
                int prevRing = ring - 2;
                int numOfSteps = (ring-1) * 4;
                float stepAngle = pi2 / float(numOfSteps);
                int innerNum = prevRing * prevRing;
                int value = int(floor((thisAngle / stepAngle) + 0.5) + float(innerNum));
                if (thisAngle <= 0.0) {
                    value += numOfSteps;
                }
                
                float x = float(_max(0,value));
                fixed2 whichDigit = (linearCoords(uv) - box) * 42.0;
                float n = 0.0;
                float offset = 0.0;
                float d1 = mod(x/100.0,10.0);
                float d2 = mod(x/10.0,10.0);
                float d3 = mod(x, 10.0);
                if (whichDigit.x > 0.0)  { n = x < 100.0 ? 0.0 : digit(d1); offset = +0.4; }
                if (whichDigit.x > 0.60) { n = x < 10.0  ? 0.0 : digit(d2);  offset =  0.0; }
                if (whichDigit.x > 1.0)  { n = digit(d3);                         offset = -0.4; }
   
                fixed2 cuv = mod(uv/4.0, 2.0) - fixed2(1.0+offset,1.0);
                
                float val = mod(x/36.0, 1.0);
                col = (0.3 + fixed3( val, val, val ) * 0.7);
                fixed4 fragColor = fixed4(lerp(character(n, cuv), col, fixed3(0.0, 0.0, 0.0)), 1) * fixed4(0.5 + d1 / 10.0, 0.5 + d2 / 10.0, 0.5 + d3 / 10.0, 1);

                return fragColor;
            }
            ENDCG
        }
    }
}
