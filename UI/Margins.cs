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

        public static Margins Horizontal(int x)
        {
            return new Margins
            {
                Top = 0,
                Bottom = 0,
                Left = x,
                Right = x,
            };
        }

        public TextureSlice Apply(TextureSlice area)
        {
            return TextureSlice.FromTwoPoints(
                area.TopLeft + (this.Left, this.Top),
                area.BottomRight - (this.Right, this.Bottom)
            );
        }

        public TextureSlice Remove(TextureSlice area)
        {
            return TextureSlice.FromTwoPoints(
                area.TopLeft - (this.Left, this.Top),
                area.BottomRight + (this.Right, this.Bottom)
            );
        }
    }
}