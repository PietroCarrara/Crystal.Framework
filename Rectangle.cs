using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework
{
    public struct Rectangle
    {
        /// <summary>
        /// The top left point of this rectangle
        /// </summary>
        public Vector2 Position;

        public float Width, Height;

        public float Left => this.Position.X;
        public float Right => this.Position.X + this.Width;
        public float Top => this.Position.Y;
        public float Bottom => this.Position.Y + this.Height;

        public Rectangle(float posX, float posY, float width, float height)
        {
            this.Position = new Vector2(posX, posY);

            this.Width = width;
            this.Height = height;
        }

        public Rectangle(Vector2 pos, float width, float height)
        : this(pos.X, pos.Y, width, height)
        { }

        public TextureSlice ToTextureSlice()
        {
            return new TextureSlice(
                (int)this.Position.X,
                (int)this.Position.Y,
                (int)this.Width,
                (int)this.Height
            );
        }

        public Rectangle Intersection(Rectangle that)
        {
            float leftX = Math.Max(this.Position.X, that.Position.X);
            float rightX = Math.Min(this.Position.X + this.Width, this.Position.X + this.Width);
            float topY = Math.Max(this.Position.Y, that.Position.Y);
            float bottomY = Math.Min(this.Position.Y + this.Height, that.Position.Y + that.Height);

            if (leftX < rightX && topY < bottomY)
            {
                return new Rectangle(
                    leftX,
                    topY,
                    rightX - leftX,
                    bottomY - topY
                );
            }
            else
            {
                throw new Exception("No overlap");
            }
        }

        public static Rectangle operator +(Rectangle left, Vector2 right)
        {
            return new Rectangle(
                left.Position + right,
                left.Width,
                left.Height
            );
        }

        public static Rectangle operator -(Rectangle left, Vector2 right)
        {
            return new Rectangle(
                left.Position - right,
                left.Width,
                left.Height
            );
        }
    }
}