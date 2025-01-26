Shader "Custom/LineRendererHorizontalLines"
{
    Properties
    {
        _BackgroundColor("Background Color", Color) = (0.5, 0.5, 0.5, 1) // Grey
        _LineColor("Line Color", Color) = (1, 1, 1, 1)                  // White
        _LineThickness("Line Thickness", Float) = 0.1
        _LineFrequency("Line Frequency", Float) = 10.0
        _Rotation("Line Rotation (Degrees)", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

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

            fixed4 _BackgroundColor;
            fixed4 _LineColor;
            float _LineThickness;
            float _LineFrequency;
            float _Rotation;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Apply rotation to UV coordinates
                float angle = radians(_Rotation);
                float2 rotatedUV;
                rotatedUV.x = cos(angle) * i.uv.x - sin(angle) * i.uv.y;
                rotatedUV.y = sin(angle) * i.uv.x + cos(angle) * i.uv.y;

                // Wrap UV to ensure no abrupt breaks
                float2 wrappedUV = frac(rotatedUV); // Keep UV coordinates in the 0-1 range

                // Generate the line pattern
                float linePattern = fmod(wrappedUV.y * _LineFrequency, 1.0);
                float lineMask = step(linePattern, _LineThickness);

                // Output the color with solid background and lines
                return lerp(_BackgroundColor, _LineColor, lineMask);
            }
            ENDCG
        }
    }
}
