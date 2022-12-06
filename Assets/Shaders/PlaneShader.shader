Shader "Custom/PlaneShader"
{
    Properties
    {
        _Color("Color", Color) = (1,0,0,1)
        _LineColor("LineColor", Color) = (0.8,0.8,0.8,1)
        _LineSize("LineSize", Range(0,1)) = 0.05
        _LineDensityX("LineDensityX", Range(0,1)) = 0.1
        _LineDensityY("LineDensityY", Range(0,1)) = 0.1
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 _Color;
            float4 _LineColor;
            float _LineSize;
            float _LineDensityX;
            float _LineDensityY;

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
                float line_x = v.tex.x / _LineDensityX;
                float in_x_line = (line_x - floor(line_x)) < _LineSize;
                float line_y = v.tex.y / _LineDensityY;
                float in_y_line = (line_y - floor(line_y)) < _LineSize;
                fixed4 col = lerp(_Color, _LineColor, 0.5 * max(in_x_line, in_y_line));
                return col;
            }

            ENDCG
        }
    }
        FallBack "Diffuse"
}
