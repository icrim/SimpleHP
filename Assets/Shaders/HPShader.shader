Shader "Custom/HPShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,0,0,1)
        _BgColor ("BackgroundColor", Color) = (0,0,0,1)
        _BorderColor ("BorderColor", Color) = (0,0,1,1)
        _BorderSizeX ("BorderSizeX", Range(0,1)) = 0.1
        _BorderSizeY ("BorderSizeY", Range(0,1)) = 0.1
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
            float4 _BorderColor;
            float _BorderSizeX;
            float _BorderSizeY;
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
                float in_x_border = max(v.tex.x < _BorderSizeX, v.tex.x > 1 - _BorderSizeX);
                float in_y_border = max(v.tex.y < _BorderSizeY, v.tex.y > 1 - _BorderSizeY);
                col = lerp(col, _BorderColor, max(in_x_border, in_y_border));
                return col;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
