namespace Crystal.Framework.Graphics
{
    public struct Color
    {
        public static Color White => new Color(255, 255, 255);
        public static Color Black => new Color(0, 0, 0);
        public static Color Transparent => new Color(255, 255, 255, 0);

        public byte R { get => Red; set => Red = value; }
        public byte G { get => Green; set => Green = value; }
        public byte B { get => Blue; set => Blue = value; }
        public byte A { get => Alpha; set => Alpha = value; }

        public byte Red, Green, Blue, Alpha;

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            this.Red = r;
            this.Green = g;
            this.Blue = b;
            this.Alpha = a;
        }
    }
}