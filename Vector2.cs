using Crystal.Framework.Math;

namespace Crystal.Framework
{
    public struct Vector2
    {
        public static Vector2 Zero { get; } = new Vector2(0, 0);
        public static Vector2 One { get; } = new Vector2(1, 1);

        public float X, Y;

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(float xy) : this(xy, xy)
        { }

        public Vector2 Transform(Matrix4 matrix)
        {
            var mat = matrix.ToFloatArray();
            
            return new Vector2(
                this.X * mat[0, 0] + this.Y * mat[1, 0] + mat[3, 0],
                this.X * mat[0, 1] + this.Y * mat[1, 1] + mat[3, 1]
            );
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X * right.X,
                left.Y * right.Y
            );
        }

        public static Vector2 operator *(Vector2 left, float right)
        {
            return new Vector2(
                left.X * right,
                left.Y * right
            );
        }

        public static Vector2 operator *(float left, Vector2 right)
        {
            return right * left;
        }

        public static Vector2 operator /(Vector2 left, float right)
        {
            return new Vector2(
                left.X / right,
                left.Y / right
            );
        }

        public static Vector2 operator /(float left, Vector2 right)
        {
            return right / left;
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X + right.X,
                left.Y + right.Y
            );
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X - right.X,
                left.Y - right.Y
            );
        }

        public static implicit operator Vector2((float, float) self)
        {
            return new Vector2(self.Item1, self.Item2);
        }

        public static explicit operator Point(Vector2 v)
        {
            return new Point((int)v.X, (int)v.Y);
        }
    }
}