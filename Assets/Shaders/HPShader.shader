Shader "Custom/HPShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,0,0,1)
        _BgColor ("BackgroundColor", Color) = (0,0,0,1)
        _Health ("Health", Range(0,1)) = 1
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 _Color;
            float4 _BgColor;
            float _Health;

            struct IData
            {
                float4 pos: POSITION;
                float4 tex: TEXCOORD0;
            };

            struct VData
            {
                float4 pos: SV_POSITION;
                float4 tex: TEXCOORD0;
            };

            VData vert(IData i)
            {
                VData v;
                v.pos = UnityObjectToClipPos(i.pos);
                v.tex = i.tex;
                return v;
            }

            fixed4 frag(VData v) : SV_Target
            {
                float in_health = v.tex.x > 1 - _Health;
                fixed4 col = lerp(_BgColor, _Color, in_health);
                return col;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
