namespace Crystal.Framework
{
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point(int xy) : this(xy, xy)
        { }
    }
}