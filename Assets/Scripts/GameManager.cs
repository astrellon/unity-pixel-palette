using UnityEngine;
using PixelPalette;
using PixelPalette.Unity;

public class GameManager : MonoBehaviour
{
    public PixelPaletteObject Result;
    public PixelPaletteObject Result2;

    private void Awake()
    {
        var palette = new Palette(4, 2, 1, 1);
        palette.SetColour(new Colour(255, 0, 0, 255), 0);
        palette.SetColour(new Colour(0, 255, 0, 255), 1);
        palette.SetColour(new Colour(0, 0, 255, 255), 2);
        palette.SetColour(new Colour(255, 255, 255, 255), 3);
        
        palette.SetColour(new Colour(127, 0, 0, 255), 0, 1);
        palette.SetColour(new Colour(0, 127, 0, 255), 1, 1);
        palette.SetColour(new Colour(0, 0, 127, 255), 2, 1);
        palette.SetColour(new Colour(127, 127, 127, 255), 3, 1);

        var data = new byte[]{0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3};
        var image = new Image(4, 4, data, palette);
        
        var data2 = new byte[]{0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3};
        var image2 = new Image(4, 4, data2, palette);
        //this.TestImage.texture = image.CreateTexture();

        //this.TestResult.material.SetTexture("_MainTex", this.TestImage.texture);
        //this.TestResult.material.SetTexture("_Palette", this.TestPalette.texture);
        //this.TestResult.material.SetVector("_PaletteDims", paletteDims);

        this.Result.Image = image;
        this.Result.Palette = palette;
        
        this.Result2.Image = image2;
        this.Result2.Palette = palette;
    }
}