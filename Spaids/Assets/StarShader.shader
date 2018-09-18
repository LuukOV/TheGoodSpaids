Shader "Custom/StarShader" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_WaveText("Wave Texture", 2D) = "white" {}
	_Transparency("Transparency", float) = 0.25
		_TimeScale("TimeScale", float) = 1
		_Strength("Wave strength", float) = 1
		_WaveSpeed("Wave speed", float) = 1
		_WaveFrequency("Wave Frequency", float) = 1
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Back

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#define UNITY_SHADER_NO_UPGRADE 1

		struct appdata
	{
		float4 vertex : POSITION;
		float4 normal : NORMAL;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	sampler2D _WaveTex;
	float _Transparency;
	float _TimeScale;
	float _Strength;
	float _WaveSpeed;
	float _WaveFrequency;

			v2f vert(appdata v)
			{
				v2f o;

				// Add/subtract a fraction of the vertex normal to its position:
				float4 modifiedPosition = v.vertex + v.normal * sin(_Time.x * _TimeScale) * _Strength;
				o.vertex = UnityObjectToClipPos(modifiedPosition);
				// Transform the point to clip space:
				float offsetY = _Strength * sin(_Time.x * _WaveSpeed * _TimeScale + v.vertex.x * _WaveFrequency);
				o.vertex.y += offsetY;


				// Copy the UVs:
				o.uv = v.uv;// +float2(sin(_Time.x * _TimeScale), _Time.x * _TimeScale);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// Sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				float brightness = 1 * max(max(col.r, col.g), col.b);
				float4 thickness = float4(1, 1, 0.7, 1 - (brightness * _Transparency));

				return thickness;
			}
			ENDCG
		}
	}
}
