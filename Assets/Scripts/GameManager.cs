using UnityEngine;
using PixelPalette;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.RawImage TestPalette;
    public UnityEngine.UI.RawImage TestImage;
    public UnityEngine.UI.RawImage TestResult;

    private void Start()
    {
        var colours = new int[]{unchecked((int)0xFF0000FF), 0x00FF00FF, 0x0000FFFF, unchecked((int)0xFFFFFFFF), 0x770000FF, 0x007700FF, 0x000077FF, 0x777777FF};
        var palette = new Palette(colours, 4, new Vector3Int(2, 1, 1));
        this.TestPalette.texture = palette.CreateTexture();

        var data = new byte[]{0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3};
        var image = new Image(4, 4, data, palette);
        this.TestImage.texture = image.CreateTexture();

        var dimSizes = palette.ExtraDimSizes;
        var paletteDims = new Vector4(palette.NumberOfBaseColours, dimSizes.x, dimSizes.y, dimSizes.z);

        this.TestResult.material.SetTexture("_MainTex", this.TestImage.texture);
        this.TestResult.material.SetTexture("_Palette", this.TestPalette.texture);
        this.TestResult.material.SetVector("_PaletteDims", paletteDims);
    }
}