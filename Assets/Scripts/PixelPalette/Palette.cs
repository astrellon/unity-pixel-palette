using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelPalette
{
    public class Palette
    {
        public readonly int[] Colours;
        public readonly byte NumberOfBaseColours;
        public readonly Vector3Int ExtraDimSizes;

        public Palette(int[] colours, byte numberOfBaseColours, Vector3Int extraDimSizes)
        {
            this.Colours = colours;
            this.NumberOfBaseColours = numberOfBaseColours;
            this.ExtraDimSizes = extraDimSizes;
        }

        public Texture2D CreateTexture()
        {
            var size = Mathf.CeilToInt(Mathf.Sqrt(this.Colours.Length));
            var result = new Texture2D(size, size, TextureFormat.RGBA32, false);
            result.filterMode = FilterMode.Point;
            var data = result.GetRawTextureData();

            for (var i = 0; i < this.Colours.Length; i++)
            {
                var pos = i * 4;
                var colour = this.Colours[i];
                data[pos    ] = (byte)((colour >> 24) & 0xFF);
                data[pos + 1] = (byte)((colour >> 16) & 0xFF);
                data[pos + 2] = (byte)((colour >>  8) & 0xFF);
                data[pos + 3] = (byte)(colour & 0xFF);
            }

            result.LoadRawTextureData(data);
            result.Apply();
            return result;
        }
    }
}