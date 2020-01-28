namespace Crystal.Framework
{
    public struct Point
    {
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

        public static implicit operator Vector2(Point self)
        {
            return new Vector2(self.X, self.Y);
        }
    }
}