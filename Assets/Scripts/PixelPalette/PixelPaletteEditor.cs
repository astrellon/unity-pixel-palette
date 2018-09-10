using UnityEngine;
using UnityEngine.UI;

namespace PixelPalette.Unity
{
    public class PixelPaletteEditor : MonoBehaviour
    {
        private Image image;

        private void Start()
        {
            this.image = this.GetComponent<PixelPaletteObject>().Image;
        }

        private void Update()
        {
            /*
            var corners = new Vector3[4];
            var rawImage = GetComponent<RawImage>();
            rawImage.rectTransform.GetWorldCorners(corners);
            var newRect = new Rect(corners[0], corners[2] - corners[0]);
            if (newRect.Contains(Input.mousePosition))
            {
                var pos = Input.mousePosition - (Vector3)newRect.position;
                var imagePosX = Mathf.FloorToInt(pos.x / rawImage.rectTransform.sizeDelta.x * this.image.Width);
                var imagePosY = Mathf.FloorToInt(pos.y / rawImage.rectTransform.sizeDelta.y * this.image.Height);
                //Debug.Log(imagePosX + ", " + imagePosY);
                //Debug.Log(pos);
                if (Input.GetMouseButtonDown(0))
                {
                    this.image.SetPixel(imagePosX, imagePosY, 1);
                }
            }
            */
        }
    }
}