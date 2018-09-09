Shader "Unlit/PixelPalette"
{
    Properties
    {
        _Palette ("Palette", 2D) = "white" {}
        _MainTex ("Image", 2D) = "white" {}
        _PaletteDims ("Palette Dimensions", Vector) = (1.0, 1.0, 1.0, 1.0)
        _PaletteOffset ("Palette Offset", Vector) = (0.0, 0.0, 0.0, 0.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _Palette;
            float4 _Palette_TexelSize;
            float4 _MainTex_ST;
            float4 _PaletteDims;
            float4 _PaletteOffset;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float baseIndex = tex2D(_MainTex, i.uv).r * 255.0;
                float index = baseIndex + 
                    _PaletteOffset.x + 
                    _PaletteOffset.y * _PaletteDims.x + 
                    _PaletteOffset.z * _PaletteDims.y * _PaletteDims.x + 
                    _PaletteOffset.w * _PaletteDims.z * _PaletteDims.y * _PaletteDims.x;

                float u = index % _Palette_TexelSize.z;
                float v = index * _Palette_TexelSize.x;

                // Small offset to fix sampling at the edges of pixels.
                fixed4 col = tex2D(_Palette, float2(u * _Palette_TexelSize.x + 0.1, v * _Palette_TexelSize.y + 0.1));

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
