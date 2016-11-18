Shader "Unlit/fragment_vertex" { // defines the name of the shader 
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Value ("Value", Range(1.,15.)) = 5.
    }
    
   SubShader { // Unity chooses the subshader that fits the GPU best
      Tags { "RenderType"="Opaque" }
      LOD 100

      Pass { // some shaders require multiple passes
         CGPROGRAM // here begins the part in Unity's Cg

         #pragma vertex vert 
            // this specifies the vert function as the vertex shader 
         #pragma fragment frag
            // this specifies the frag function as the fragment shader

         #include "UnityCG.cginc"
            
         struct appdata
         {
             float4 vertex : POSITION;
             float2 uv : TEXCOORD0;
             float4 normal : NORMAL;
         };
            
		 struct v2f {
		 	float4 vertex : SV_POSITION;
		 	float2 uv : TEXCOORD0;
		 };

    	 sampler2D _MainTex;
         float4 _MainTex_ST;
		 float _Value;
            
         v2f vert(appdata v)
            // vertex shader 
         {
			v2f o;
            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			
			float d = tex2Dlod(_MainTex, float4(v.uv, 0, 0)).r * _Value;
			o.vertex.xyz += v.normal * d;	
            return o;
         }

         float4 frag(v2f i) : COLOR // fragment shader
         {
            fixed4 col = tex2D(_MainTex, i.uv);
            float height = col.r * _Value;

            if(height < 0.2) {
            	col.b += 0.4;
            	col.g += 0.15;
            }
            if(height > 0.2) {
            	col.g += 0.3;
            }
            if(height > 1.5) {
            	col.rb += 0.3;
            }
            return col; 
            
         }

         ENDCG // here ends the part in Cg 
      }
   }
}