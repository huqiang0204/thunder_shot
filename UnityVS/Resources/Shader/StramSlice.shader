Shader "Custom/StramSlice" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SliceTex("Slice ",2D) = "white"{}
	    _Speed("Seed", Range(0,1000)) = 0.5
		_Minx("Minx", Range(0,1)) = 0
		_Maxx("Maxx", Range(0,1)) = 1
		_Miny("Miny", Range(0,1)) = 0
		_Maxy("Maxy", Range(0,1)) = 1
		_Pos("posision", Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		float _Speed;
		float _Minx;
		float _Maxx;
		float _Miny;
		float _Maxy;
		float _Pos;
		sampler2D _SliceTex;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {

			fixed y = _Time*_Speed;
			fixed2 f = IN.uv_MainTex;
			//y = _Pos;
			f.y -= y;
			if (f.y < _Miny) {
				f.y = _Miny + fmod(abs(f.y), _Maxy - _Miny);
			}
			fixed4 c = tex2D(_MainTex, f);// *_Color;
										  //o.Albedo = c.rgb;
			o.Emission = c.rgb;
			// Metallic and smoothness come from slider variables
			float x = (IN.uv_MainTex.x - _Minx) / (_Maxx - _Minx);
			fixed4 s = tex2D(_SliceTex, float2(x, 1));
			x = s.r*255 + s.g ;
			o.Alpha = IN.uv_MainTex.y*255> x? 0 : c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
