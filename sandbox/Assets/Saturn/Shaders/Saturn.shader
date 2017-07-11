Shader "Custom/Saturn" {
  Properties {
_MainTex ("Texture", 2D) = "white" {}
_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
_RimPower ("Rim Power", Range(0.5,18.0)) = 3.0

}
SubShader {
Tags { "Queue" = "AlphaTest" "IgnoreProjector"="False"  }

CGPROGRAM
#pragma surface surf Lambert
#include "UnityCG.cginc"

struct Input {
	float2 uv_MainTex;
	float3 worldRefl;
	float3 viewDir;
};
sampler2D _MainTex;
float4 _RimColor;
float _RimPower;
//fixed4 _LightColor0;
void surf (Input IN, inout SurfaceOutput o) {
	o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb *0.8;
	half rim = (1 - saturate(dot (normalize(IN.viewDir), o.Normal)));
	o.Emission = ((_RimColor.rgb) * pow (rim, _RimPower))*-0.8 ;
	//o.Alpha = -(pow (rim, _AlphPower))+_AlphaMin ;
	//o.Alpha = 1;
}
ENDCG
}
Fallback "VertexLit"
}