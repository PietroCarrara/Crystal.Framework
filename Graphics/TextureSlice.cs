using System.Numerics;
using System.Linq;
using System.Collections.Generic;

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

        public bool Contains(Vector2 point)
        {
            var bottomRight = this.BottomRight;

            return point.X >= this.TopLeft.X &&
                   point.Y >= this.TopLeft.Y &&
                   point.X <= bottomRight.X &&
                   point.Y <= bottomRight.Y;
        }

        /// <summary>
        /// Returns the scale that should be applied to the width and height of this
        /// rectangle to be able to fit in a area of informed size, respecting the
        /// aspect ratio
        /// </summary>
        /// <param name="area">The size of the area this rectangle should fit</param>
        public float Fit(Vector2 area)
        {
            var scaleX = area.X / this.Width;
            var scaleY = area.Y / this.Height;

            return System.Math.Min(scaleY, scaleX);
        }

        /// <summary>
        /// Returns the scale that should be applied to the width and height of this rectangle,
        /// so that only one of the axes does not leak out of the area, respecting aspect ratio
        /// </summary>
        /// <param name="area">The area to fit</param>
        public float Crop(Vector2 area)
        {
            var scaleX = area.X / this.Width;
            var scaleY = area.Y / this.Height;

            return System.Math.Max(scaleY, scaleX);
        }

        public override string ToString()
        {
            return $"TextureSlice {{ TopLeft = {TopLeft}, Width = {Width}, Height = {Height} }}";
        }

        public static bool operator ==(TextureSlice left, TextureSlice right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TextureSlice left, TextureSlice right)
        {
            return !(left == right);
        }

        public static TextureSlice operator +(TextureSlice left, Point right)
        {
            return new TextureSlice(
                left.TopLeft + right,
                left.Size
            );
        }

        public static TextureSlice operator +(Point left, TextureSlice right)
        {
            return right + left;
        }

        /// <summary>
        /// Creates a texture slice that has the requested top left and bottom right
        /// </summary>
        /// <returns>A texture slice that has the requested top left and bottom right</returns>
        public static TextureSlice FromTwoPoints(Point topLeft, Point bottomRight)
        {
            var size = bottomRight - topLeft;

            return new TextureSlice(
                topLeft,
                size
            );
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

        public bool Equals(TextureSlice t)
        {
            return this.TopLeft == t.TopLeft &&
                   this.Width == t.Width &&
                   this.Height == t.Height;
        }

        public override int GetHashCode()
        {
            var hash = 13;

            hash = hash * 7 + this.TopLeft.GetHashCode();
            hash = hash * 7 + this.Width.GetHashCode();
            hash = hash * 7 + this.Height.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is TextureSlice t)
            {
                return this.Equals(t);
            }

            return false;
        }
    }
}