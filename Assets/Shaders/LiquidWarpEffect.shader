Shader "MyShaders/LiquidWarpEffect"
{
    Properties {
		_MainTex ("", 2D) = "white" {}
		_CenterX ("CenterX", Float) = 0
		_CenterY ("CenterY", Float) = 0
		_Radius ("Radius", Range(-1,1)) = -1.0
		_Amplitude ("Amplitude", Range(-10,10)) = 0.05
	}
	 
	SubShader {
	 
		ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings
	 
	 	Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			//we include "UnityCG.cginc" to use the appdata_img struct
			
			float _CenterX;
			float _CenterY;
			float _Radius;
			float _Amplitude;

			struct v2f {
				float4 pos : POSITION;
				half2 uv : TEXCOORD0;
			};
	   
			//Our Vertex Shader
			v2f vert (appdata_img v){
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
				return o;
			}

			sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders

			//Our Fragment Shader
			fixed4 frag (v2f i) : COLOR{
                float waveSpeed = 0.5;
                float wavesize = 0.08;

				float2 diff = float2(_CenterX/_ScreenParams.x, _CenterY/_ScreenParams.y);
				float2 distVec = i.uv - diff;
                float aspectRatio = _ScreenParams.x/_ScreenParams.y;
                distVec.x *= aspectRatio;
                float dist = length(distVec);

				float2 uv_displaced = i.uv;

				if (dist>_Radius) {
					if (dist<_Radius+wavesize) {
                        float multiplier = (dist < 0.5) ? ((dist-1.0)*(dist-1.0)) : 0.0;
						float angle=(dist-_Radius)*2.0*3.141592654/wavesize;
						float cossin=(1.0-cos(angle))*0.5;
						uv_displaced.x-=cossin*diff.x*_Amplitude/dist * waveSpeed * multiplier;
						uv_displaced.y-=cossin*diff.y*_Amplitude/dist * waveSpeed * multiplier;
					}
				}
				
				fixed4 orgCol = tex2D(_MainTex, uv_displaced); //Get the orginal rendered color
				return orgCol;

                /*float waveStrength = 0.05;
                float frequency = 30.0;
                float waveSpeed = 10.0;
                float4 sunlightColor = float4(1.0,0.91,0.75, 1.0);
                float sunlightStrength = 0.5;
                //
                
                float2 tapPoint = float2(_CenterX/_ScreenParams.x, _CenterY/_ScreenParams.y);
                float2 uv = i.uv;
                float modifiedTime = _Time.y * waveSpeed;
                float aspectRatio = _ScreenParams.x/_ScreenParams.y;
                float2 distVec = uv - tapPoint;
                distVec.x *= aspectRatio;
                float distance = length(distVec);
                float2 newTexCoord = uv;
                
                float multiplier = (distance < 0.3) ? ((distance-1.0)*(distance-1.0)) : 0.0;
                float addend = (sin(frequency*distance-modifiedTime)+1.0) * waveStrength * multiplier;
                newTexCoord += addend;    
                
                float4 colorToAdd = sunlightColor * sunlightStrength * addend;
                
                float4 fragColor = tex2D(_MainTex, newTexCoord) + colorToAdd;

                return fragColor;*/
			}
			ENDCG
		}
	}
	
	FallBack "Diffuse"
	
}
