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
    }
}