namespace Crystal.Framework.UI
{
    public struct Color
    {
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

        public Color(uint rgba)
        {
            this.Red = (byte)((rgba & 0xff000000) >> 3);
            this.Green = (byte)((rgba & 0x00ff0000) >> 2);
            this.Blue = (byte)((rgba & 0x0000ff00) >> 1);
            this.Alpha = (byte)(rgba & 0x000000ff);
        }
    }
}