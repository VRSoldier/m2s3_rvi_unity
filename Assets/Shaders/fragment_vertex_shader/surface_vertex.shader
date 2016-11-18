Shader "Custom/surface_vertex" {
	Properties {
		_Relief ("Relief", 2D) = "gray" {}
		_Value ("Value", Range(2., 15.0)) = 5.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:disp

		sampler2D _Relief;
		float _Value;

		struct Input {
			float2 uv_Relief;
			float3 worldPos;
		};
		
		void disp (inout appdata_full v) {
			float d = tex2Dlod(_Relief, float4(v.texcoord.xy, 0, 0)).r * _Value;
			v.vertex.xyz += v.normal * d;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			float height = IN.worldPos.y;
			half4 c;
			c.r = 0.;
			c.g = 0.;
			c.b = 0.;
			if(height < 1) {
				c.b = 1. - height;
			}
			if(height > 0.2) {
				c.g = height;
			}
			if(height > 2) {
				c.r = height - 2;
				c.b = height - 2;
			}
			c.a = 1;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
