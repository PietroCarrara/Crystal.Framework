namespace Crystal.Framework
{
    public struct Point
    {
        public static Point Zero { get; } = new Point(0, 0);
        
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

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public static implicit operator Vector2(Point self)
        {
            return new Vector2(self.X, self.Y);
        }
    }
}