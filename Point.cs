using System.Numerics;

namespace Crystal.Framework
{
    public struct Point
    {
        public static Point Zero { get; } = new Point(0, 0);
        public static Point One { get; } = new Point(1, 1);

        public int X, Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point(int xy) : this(xy, xy)
        { }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public static Point operator *(Point left, int right)
        {
            return new Point(left.X * right, left.Y * right);
        }

        public static Point operator *(int left, Point right)
        {
            return right * left;
        }

        public static Point operator /(Point left, int right)
        {
            return new Point(left.X / right, left.Y / right);
        }

        public static implicit operator Point((int, int) self)
        {
            return new Point(self.Item1, self.Item2);
        }

        public static implicit operator Vector2(Point self)
        {
            return new Vector2(self.X, self.Y);
        }

        public static implicit operator Vector3(Point self)
        {
            return new Vector3(self.X, self.Y, 0);
        }

        public static explicit operator Point(Vector2 v)
        {
            return new Point((int)v.X, (int)v.Y);
        }

        public override int GetHashCode()
        {
            var hash = 11;

            hash = 7 * hash + this.X;
            hash = 7 * hash + this.Y;

            return hash;
        }

        public bool Equals(Point p)
        {
            return this.X == p.X &&
                   this.Y == p.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point p)
            {
                return this.Equals(p);
            }

            return false;
        }
    }
}