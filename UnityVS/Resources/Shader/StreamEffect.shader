Shader "Custom/StreamEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	_SliceTex("Slice ",2D) = "white"{}
	_Speed("Seed", Range(0,1000)) = 0.5
		_Minx("Minx", Range(0,1)) = 0
		_Maxx("Maxx", Range(0,1)) = 1
		_Miny("Miny", Range(0,1)) = 0
		_Maxy("Maxy", Range(0,1)) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" }
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
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Speed;
			float _Minx;
			float _Maxx;
			float _Miny;
			float _Maxy;
			sampler2D _SliceTex;

			fixed4 frag (v2f i) : SV_Target
			{
		    fixed y = _Time*_Speed;
			fixed2 f = i.uv;
			//y = _Pos;
			f.y -= y;
			if (f.y < _Miny) {
				f.y = _Miny + fmod(abs(f.y), _Maxy - _Miny);
			}
			fixed4 c = tex2D(_MainTex, f);// *_Color;
										  //o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			float x = (i.uv.x - _Minx) / (_Maxx - _Minx);
			fixed4 s = tex2D(_SliceTex, float2(x, 1));
			x = s.r + s.g / 255;
			if (i.uv.y> x)
				c.a = 0;
		    return c;
			}
			ENDCG
		}
	}
}
