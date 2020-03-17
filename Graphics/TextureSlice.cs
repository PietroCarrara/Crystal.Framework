using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Crystal.Framework;

namespace Crystal.Framework.Graphics
{
    public struct TextureSlice
    {
        public Point TopLeft;

        public int Width;
        public int Height;

        public Point BottomRight => TopLeft + new Point(Width, Height);

        public int Area => Width * Height;

        public Point Size
        {
            get => new Point(Width, Height);
            set
            {
                this.Width = value.X;
                this.Height = value.Y;
            }
        }

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

        public static Vector2 operator *(TextureSlice left, Vector2 right)
        {
            return new Vector2(
                left.Width * right.X,
                left.Height * right.Y
            );
        }

        public static Vector2 operator *(Vector2 left, TextureSlice right)
        {
            return right * left;
        }

        /// <summary>
        /// Returns the smallest TextureSlice that contains all others
        /// </summary>
        /// <param name="rects">TextureSlices to contain</param>
        /// <returns>Smallest TextureSlice that contains all others</returns>
        public static TextureSlice Union(params TextureSlice[] rects)
        {
            return Union(rects.AsEnumerable());
        }

        /// <summary>
        /// Returns the smallest TextureSlice that contains all others
        /// </summary>
        /// <param name="rects">TextureSlices to contain</param>
        /// <returns>Smallest TextureSlice that contains all others</returns>
        public static TextureSlice Union(IEnumerable<TextureSlice> rects)
        {
            var first = rects.First();
            var topLeft = first.TopLeft;
            var bottomRight = first.BottomRight;

            foreach (var rect in rects.Skip(1))
            {
                if (rect.TopLeft.X < topLeft.X)
                {
                    topLeft.X = rect.TopLeft.X;
                }

                if (rect.TopLeft.Y < topLeft.Y)
                {
                    topLeft.Y = rect.TopLeft.Y;
                }

                if (rect.BottomRight.X > bottomRight.X)
                {
                    bottomRight.X = rect.BottomRight.X;
                }

                if (rect.BottomRight.Y > bottomRight.Y)
                {
                    bottomRight.Y = rect.BottomRight.Y;
                }
            }

            return new TextureSlice(
                topLeft,
                bottomRight - topLeft
            );
        }
    }
}