namespace PixelPalette
{
    public struct Colour
    {
        public static readonly Colour Empty = new Colour(255, 0, 255, 127);

        public readonly byte Red;
        public readonly byte Green;
        public readonly byte Blue;
        public readonly byte Alpha;

        public Colour(byte red, byte green, byte blue, byte alpha = 255)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
            this.Alpha = alpha;
        }
    }
}