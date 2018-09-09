using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelPalette
{
    public class Image
    {
        public readonly int Width;
        public readonly int Height;
        public readonly byte[] PixelIndices;
        public readonly Palette Palette;

        public Image(int width, int height, byte[] pixelIndices, Palette palette)
        {
            this.Width = width;
            this.Height = height;
            this.PixelIndices = pixelIndices;
            this.Palette = palette;
        }

        public Texture2D CreateTexture()
        {
            var result = new Texture2D(this.Width, this.Height, TextureFormat.R8, false);
            result.filterMode = FilterMode.Point;
            var data = result.GetRawTextureData();

            Array.Copy(this.PixelIndices, data, this.PixelIndices.Length);

            result.LoadRawTextureData(data);
            result.Apply(false);
            return result;
        }
    }
}