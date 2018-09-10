using System;
using UnityEngine;

namespace PixelPalette.Unity
{
    public class PixelPaletteObject : MonoBehaviour
    {
        public Image Image;
        public Palette Palette;
        private Material material;
        private Texture2D prevPaletteTexture;
        private Texture2D prevImageTexture;

        private void Start()
        {
            this.material = this.GetMaterial();
        }

        private Material GetMaterial()
        {
            var ui = this.GetComponent<UnityEngine.UI.Graphic>();
            if (ui != null)
            {
                return ui.material;
            }

            var renderer = this.GetComponent<Renderer>();
            if (renderer != null)
            {
                return renderer.material;
            }

            throw new Exception("Cannot apply palette");
        }

        private void Update()
        {
            if (this.Palette == null || this.Image == null)
            {
                return;
            }

            if (this.Palette.IsDirty || this.Palette.Texture != this.prevPaletteTexture || 
                this.Image.IsDirty || this.Image.Texture != this.prevImageTexture)
            {
                if (this.Palette.IsDirty)
                {
                    this.Palette.Update();
                }
                if (this.Image.IsDirty)
                {
                    this.Image.Update();
                }

                this.UpdateTextures();
            }
        }

        private void UpdateTextures()
        {
            if (this.Image == null || this.Palette == null)
            {
                return;
            }

            var paletteDims = new Vector4(this.Palette.BaseColourMap.Count, this.Palette.Dim1Size, this.Palette.Dim2Size, this.Palette.Dim3Size);

            this.prevImageTexture = this.Image.Texture;
            this.prevPaletteTexture = this.Palette.Texture;

            this.material.SetTexture("_MainTex", this.Image.Texture);
            this.material.SetTexture("_Palette", this.Palette.Texture);
            this.material.SetVector("_PaletteDims", paletteDims);
        }
    }
}