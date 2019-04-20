// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Radial Color Change"
{
	Properties
	{
		_Speed("Effect Speed", float) = 1
		_Intensity("Effect Intensity", float) = 5
		_AxisScalars("Axis Scalars", Vector) = (1, 0, 1, 0)
		_Color1("Color 1", Color) = (1, 0, 1, 1)
		_Color2("Color 2", Color) = (0, 1, 1, 1)
	}

		SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"

			float _Speed;
			float _Intensity;
			float4 _AxisScalars;
			float4 _Color1;
			float4 _Color2;

			struct MyStruct
			{
				float4 worldPosition : SV_POSITION;
				float3 localPosition : TEXCOORD0;
			};

			MyStruct MyVertexProgram(float4 position : POSITION, float3 localPosition : TEXCOORD0)
			{
				MyStruct retval;
				retval.worldPosition = UnityObjectToClipPos(position);
				retval.localPosition = position.xyz;

				return retval;
				
			}

			float4 MyFragmentProgram(MyStruct myStruct) : SV_TARGET
			{
				float index = abs(sin(_Intensity * (length(float3(_AxisScalars.x * myStruct.localPosition.x, _AxisScalars.y * myStruct.localPosition.y, _AxisScalars.z * myStruct.localPosition.z)) - _Speed * _Time.y)));
				
				float4 finalColor = index * _Color1 + (1 - index) * _Color2;

				return finalColor;
			}

			ENDCG
		}
	}
}