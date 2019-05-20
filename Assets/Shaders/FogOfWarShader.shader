Shader "Fuck/FogOfWarShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Transparency("Transparency", Range(0.0,1.0)) = 0.5
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		
		#pragma surface surf Standard fullforwardshadows
		//#pragma fragment frag
		
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		struct v2f{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float3 _Position = (1.0, 1.0, 1.0);
		float _Radius = 5;
		float _Transparency;
		float offset = 0;

		UNITY_INSTANCING_BUFFER_START(Props)
			
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			offset = _Time / 100;
			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex * offset) * _Color;
			o.Albedo = c.rgb;
		
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

			if(IN.worldPos.x > (_Position.x - _Radius) && IN.worldPos.x < (_Position.x + _Radius) && IN.worldPos.z >(_Position.z - _Radius) && IN.worldPos.z < (_Position.z + _Radius)){
				clip(IN.worldPos - _Radius);
			}
		}

		fixed4 frag(v2f i) : SV_Target
		{
			// sample the texture
			fixed4 col = tex2D(_MainTex, i.uv) * _Color;
			col.a = _Transparency;
			return col;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
