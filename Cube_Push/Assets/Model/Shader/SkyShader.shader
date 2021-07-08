Shader "Game/SkyShader"
{
    Properties
    {
        _BGTopColor("Top",Color) = (1,1,1,1)
        _BGBottomColor("Bottom",Color) = (0,0,0,1)
        _Offset("UVOffsetY",Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "PreviewType" = "Skybox" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc" 

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

            fixed4 _BGTopColor;
            fixed4 _BGBottomColor;
            float _Offset;

            float DrawCircle(float2 uvIn, float2 pos, float radius, float angle)
            {
                float2 uv = uvIn;
                uv.x *= 7.4;
                float2 d = uv - pos;
                return saturate(smoothstep(0, 0.01, radius - length(d)));
            }
           
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                i.uv.y += _Offset;
            //i.uv.y = i.uv.y * 0.5 + 0.5;
                fixed4 col = lerp(_BGBottomColor, _BGTopColor,i.uv.y);
                //float c1 = DrawCircle(i.uv, float2(0.5, 0.5), 0.2,1);
               // float c1 =saturate( DrawSin(i.uv));
               // col = saturate(1 - c1) * col;
               // col += c1 * _Color1;
                return col;
            }
            ENDCG
        }
    }
}
