Shader "Unlit/GPUHash2To2UintVisualizer"
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
            #include "Assets/GPUHash/Hash2To2Uint.cginc"

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
                uint2 input = i.vertex.xy;
                float2 c = pcg2d(input) / float(0xffffffffu);
                return float4(c, 1.0, 1.0);
            }
            ENDCG
        }
    }
}
