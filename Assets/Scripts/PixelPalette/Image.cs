using System;
using UnityEngine;

namespace PixelPalette
{
    public class Image
    {
        public int Width {get; private set;}
        public int Height {get; private set;}
        public byte[] PixelIndices {get; private set;}
        public Palette Palette {get; private set;}
        public Texture2D Texture {get; private set;}
        public bool IsDirty {get; private set;}

        public Image(int width, int height, byte[] pixelIndices, Palette palette)
        {
            this.Width = width;
            this.Height = height;
            this.PixelIndices = pixelIndices;
            this.Palette = palette;

            this.Update();
        }

        public void SetPixel(int x, int y, byte pixel)
        {
            this.PixelIndices[y * this.Width + x] = pixel;
            this.IsDirty = true;
        }

        public void Update()
        {
            this.IsDirty = false;
            this.Texture = this.CreateTexture();
        }
        
        private Texture2D CreateTexture()
        {
            var result = new Texture2D(this.Width, this.Height, TextureFormat.R8, false);
            result.filterMode = FilterMode.Point;
            var data = result.GetRawTextureData();

            Array.Copy(this.PixelIndices, data, this.PixelIndices.Length);

            result.LoadRawTextureData(data);
            result.Apply(false, true);
            return result;
        }
    }
}