Shader "Unlit/GPUHashVisualizer"
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
            #include "Assets/GPUHash/GPUHash.cginc"

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
                uint4 seed = uint4(i.vertex.x, i.vertex.y, uint(i.vertex.x) ^ uint(i.vertex.y), i.vertex.x + i.vertex.y);
                uint3 input = seed.xyz;
                float c = pcg3d(input);
                return float4(c, c, c, 1.0);
            }
            ENDCG
        }
    }
}
