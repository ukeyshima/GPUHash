Shader "Unlit/GPUHash4To4FloatVisualizer"
{
    Properties{ }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Assets/GPUHash/HashUtility.cginc"
            #include "Assets/GPUHash/Hash4To4Float.cginc"

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 input = i.uv;
                float4 c = hashwithoutsine44(float4(input, 1.0, 1.0));
                return float4(c);
            }
            ENDCG
        }
    }
}
