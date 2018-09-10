using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelPalette
{
    public class Palette
    {
        public static readonly Color32 Empty = new Color32(255, 0, 255, 127);
        public readonly Dictionary<int, Color32> Colours;
        public readonly byte NumberOfBaseColours;
        public readonly byte Dim1Size;
        public readonly byte Dim2Size;
        public readonly byte Dim3Size;

        public Palette(Dictionary<int, Color32> colours, byte numBaseColours, byte dim1Size = 1, byte dim2Size = 1, byte dim3Size = 1)
        {
            this.Colours = colours;
            this.NumberOfBaseColours = numBaseColours;
            this.Dim1Size = dim1Size;
            this.Dim2Size = dim2Size;
            this.Dim3Size = dim3Size;
        }

        public Palette(byte numBaseColours, byte dim1Size = 1, byte dim2Size = 1, byte dim3Size = 1)
        {
            this.Colours = new Dictionary<int, Color32>();
            this.NumberOfBaseColours = numBaseColours;
            this.Dim1Size = dim1Size;
            this.Dim2Size = dim2Size;
            this.Dim3Size = dim3Size;
        }

        public void SetColour(Color32 colour, byte baseColour, byte dim1 = 0, byte dim2 = 0, byte dim3 = 0)
        {
            var index = dim3 << 24 | dim2 << 16 | dim1 << 8 | baseColour;
            this.Colours[index] = colour;
        }

        public Color32 GetColour(byte baseColour, byte dim1 = 0, byte dim2 = 0, byte dim3 = 0)
        {
            var result = Empty;
            var index = dim3 << 24 | dim2 << 16 | dim1 << 8 | baseColour;

            if (this.Colours.TryGetValue(index, out result))
            {
                return result;
            }

            this.Colours.TryGetValue(baseColour, out result);
            return result;
        }

        public Dictionary<byte, byte> GetBaseWithNoGaps()
        {
            byte accum = 0;
            var result = new Dictionary<byte, byte>();
            for (byte b = 0; b < this.NumberOfBaseColours; b++)
            {
                if (this.Colours.ContainsKey(b))
                {
                    result[b] = accum++;
                }
            }
            return result;
        }

        public Texture2D CreateTexture()
        {
            var baseColourMap = this.GetBaseWithNoGaps();

            var totalItems = baseColourMap.Count * this.Dim1Size * this.Dim2Size * this.Dim3Size;
            var rootSize = Mathf.CeilToInt(Mathf.Sqrt(totalItems));
            var width = rootSize;
            var height = rootSize;

            var result = new Texture2D(width, height, TextureFormat.RGBA32, false);
            result.filterMode = FilterMode.Point;
            var data = result.GetRawTextureData();

            var pos = 0;
            for (byte z = 0; z < this.Dim3Size; z++)
            for (byte y = 0; y < this.Dim2Size; y++)
            for (byte x = 0; x < this.Dim1Size; x++)
            //for (byte b = 0; b < baseColourMap.Count; b++)
            foreach (var b in baseColourMap.Values)
            {
                var colour = this.GetColour(b, x, y, z);
                data[pos    ] = colour.r;
                data[pos + 1] = colour.g;
                data[pos + 2] = colour.b;
                data[pos + 3] = colour.a;
                pos += 4;
            }

            // Fill in gaps
            for (var i = totalItems; i < width * height; i++)
            {
                pos = i * 4;
                data[pos] = 0xFF;
                data[pos + 1] = 0x00;
                data[pos + 2] = 0xFF;
                data[pos + 3] = 0xFF;
            }

            result.LoadRawTextureData(data);
            result.Apply(false, true);
            return result;
        }
    }
}