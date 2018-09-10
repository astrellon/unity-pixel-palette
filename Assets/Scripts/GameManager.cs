using UnityEngine;
using PixelPalette;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.RawImage TestPalette;
    public UnityEngine.UI.RawImage TestImage;
    public UnityEngine.UI.RawImage TestResult;

    private void Start()
    {
        var palette = new Palette(255, 8, 8, 4);
        palette.SetColour(new Color32(255, 0, 0, 255), 0);
        palette.SetColour(new Color32(0, 255, 0, 255), 1);
        palette.SetColour(new Color32(0, 0, 255, 255), 2);
        palette.SetColour(new Color32(255, 255, 255, 255), 3);
        
        palette.SetColour(new Color32(127, 0, 0, 255), 0, 1);
        palette.SetColour(new Color32(0, 127, 0, 255), 1, 1);
        palette.SetColour(new Color32(0, 0, 127, 255), 2, 1);
        palette.SetColour(new Color32(127, 127, 127, 255), 3, 1);
        this.TestPalette.texture = palette.CreateTexture();

        var data = new byte[]{0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3};
        var image = new Image(4, 4, data, palette);
        this.TestImage.texture = image.CreateTexture();

        var paletteDims = new Vector4(palette.NumberOfBaseColours, palette.Dim1Size, palette.Dim2Size, palette.Dim3Size);

        this.TestResult.material.SetTexture("_MainTex", this.TestImage.texture);
        this.TestResult.material.SetTexture("_Palette", this.TestPalette.texture);
        this.TestResult.material.SetVector("_PaletteDims", paletteDims);
    }
}