Shader "Custom/vertex_shader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Value ("Value", Range(-0.2,0.2)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		
		fixed4 _Color;
		half _Value;
		
		void vert (inout appdata_full v) {
			v.vertex.xyz += v.normal * _Value;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
