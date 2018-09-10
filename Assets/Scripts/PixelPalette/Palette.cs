using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelPalette
{
    public class Palette
    {
        public readonly Dictionary<int, Colour> Colours;
        public byte NumberOfBaseColours {get; private set;}
        public byte Dim1Size {get; private set;}
        public byte Dim2Size {get; private set;}
        public byte Dim3Size {get; private set;}

        public Texture2D Texture {get; private set;}
        public Dictionary<byte, byte> BaseColourMap {get; private set;}
        public bool IsDirty {get; private set;}

        public Palette(Dictionary<int, Colour> colours, byte numBaseColours, byte dim1Size = 1, byte dim2Size = 1, byte dim3Size = 1)
        {
            this.Colours = colours;
            this.NumberOfBaseColours = numBaseColours;
            this.Dim1Size = dim1Size;
            this.Dim2Size = dim2Size;
            this.Dim3Size = dim3Size;

            this.IsDirty = true;
        }

        public Palette(byte numBaseColours, byte dim1Size = 1, byte dim2Size = 1, byte dim3Size = 1)
        {
            this.Colours = new Dictionary<int, Colour>();
            this.NumberOfBaseColours = numBaseColours;
            this.Dim1Size = dim1Size;
            this.Dim2Size = dim2Size;
            this.Dim3Size = dim3Size;
            
            this.IsDirty = true;
        }

        public void SetColour(Colour colour, byte baseColour, byte dim1 = 0, byte dim2 = 0, byte dim3 = 0)
        {
            var index = dim3 << 24 | dim2 << 16 | dim1 << 8 | baseColour;
            this.Colours[index] = colour;

            this.IsDirty = true;
        }

        public Colour GetColour(byte baseColour, byte dim1 = 0, byte dim2 = 0, byte dim3 = 0)
        {
            var result = Colour.Empty;
            var index = dim3 << 24 | dim2 << 16 | dim1 << 8 | baseColour;

            if (this.Colours.TryGetValue(index, out result))
            {
                return result;
            }

            this.Colours.TryGetValue(baseColour, out result);
            return result;
        }

        public void Update()
        {
            this.IsDirty = false;
            this.BaseColourMap = this.GetBaseWithNoGaps();
            this.Texture = this.CreateTexture();
        }

        private Dictionary<byte, byte> GetBaseWithNoGaps()
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

        private Texture2D CreateTexture()
        {
            var totalItems = this.BaseColourMap.Count * this.Dim1Size * this.Dim2Size * this.Dim3Size;
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
            foreach (var b in this.BaseColourMap.Values)
            {
                var colour = this.GetColour(b, x, y, z);
                data[pos    ] = colour.Red;
                data[pos + 1] = colour.Green;
                data[pos + 2] = colour.Blue;
                data[pos + 3] = colour.Alpha;
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