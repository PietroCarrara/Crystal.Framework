using Crystal.Framework;

namespace Crystal.Framework.Graphics
{
    public struct TextureSlice
    {
        public Point TopLeft;

        public int Width;
        public int Height;

        public TextureSlice(int topLeftX, int topLeftY, int width, int height)
        {
            this.TopLeft = new Point(topLeftX, topLeftY);

            this.Width = width;
            this.Height = height;
        }

        public TextureSlice(Point topLeft, int width, int height) :
        this(topLeft.X, topLeft.Y, width, height)
        { }

        public TextureSlice(Point topLeft, Point size) :
        this(topLeft.X, topLeft.Y, size.X, size.Y)
        { }

        public override string ToString()
        {
            return $"TextureSlice {{ TopLeft = {TopLeft}, Width = {Width}, Height = {Height} }}";
        }

        public static implicit operator Rectangle(TextureSlice self)
        {
            return new Rectangle(
                self.TopLeft,
                self.Width,
                self.Height
            );
        }
    }
}