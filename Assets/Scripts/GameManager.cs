using UnityEngine;
using PixelPalette;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.RawImage TestPalette;
    public UnityEngine.UI.RawImage TestImage;
    public GameObject TestResult;

    private void Start()
    {
        var colours = new int[]{unchecked((int)0xFF0000FF), 0x00FF00FF, 0x0000FFFF, unchecked((int)0xFFFFFFFF)};
        var palette = new Palette(colours, 4, Vector3Int.one);
        this.TestPalette.texture = palette.CreateTexture();

        var data = new byte[]{0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3};
        var image = new Image(4, 4, data, palette);
        this.TestImage.texture = image.CreateTexture();

        var renderer = this.TestResult.GetComponent<Renderer>();
        renderer.material.SetTexture("_MainTex", this.TestImage.texture);
        renderer.material.SetTexture("_Palette", this.TestPalette.texture);
        var dimSizes = palette.ExtraDimSizes;
        var paletteDims = new Vector4(palette.NumberOfBaseColours, dimSizes.x, dimSizes.y, dimSizes.z);
        renderer.material.SetVector("_PaletteDims", paletteDims);
    }
}