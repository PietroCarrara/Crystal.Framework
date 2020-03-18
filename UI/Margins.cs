using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public struct Margins
    {
        public int Top, Bottom, Left, Right;

        public static Margins All(int all)
        {
            return new Margins
            {
                Top = all,
                Bottom = all,
                Left = all,
                Right = all,
            };
        }

        public static Margins XY(int x, int y)
        {
            return new Margins
            {
                Top = y,
                Bottom = y,
                Left = x,
                Right = x
            };
        }

        public TextureSlice Apply(TextureSlice area)
        {
            return new TextureSlice(
                area.TopLeft.X + this.Left,
                area.TopLeft.Y + this.Top,
                area.Width - this.Right,
                area.Height - this.Bottom
            );
        }
    }
}