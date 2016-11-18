Shader "Custom/tp_surface_shader_1" {
	Properties {
		_FirstTex ("First (RGB)", 2D) = "green" {}
		_SecondTex ("Second (RGB)", 2D) = "red" {}
		_Value ("Value (%)", float) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _FirstTex;
		sampler2D _SecondTex;
		float _Value;

		struct Input {
			float2 uv_FirstTex;
			float2 uv_SecondTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c1 = tex2D (_FirstTex, IN.uv_FirstTex);
			half4 c2 = tex2D (_SecondTex, IN.uv_SecondTex);
			o.Albedo = c1.rgb * (1. - _Value) + c2.rgb * (_Value);
			o.Alpha = c1.a * (1. - _Value) + c2.a * (_Value);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
