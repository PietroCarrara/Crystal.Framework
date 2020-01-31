namespace Crystal.Framework.UI
{
    public struct AnchorPoint
    {
        public static AnchorPoint TopLeft => new AnchorPoint(0, 0);
        public static AnchorPoint TopCenter => new AnchorPoint(.5f, 0);
        public static AnchorPoint TopRight => new AnchorPoint(1, 0);
        public static AnchorPoint CenterLeft => new AnchorPoint(0, .5f);
        public static AnchorPoint Center => new AnchorPoint(.5f, .5f);
        public static AnchorPoint CenterRight => new AnchorPoint(1, .5f);
        public static AnchorPoint BottomLeft => new AnchorPoint(0, 1);
        public static AnchorPoint BottomCenter => new AnchorPoint(.5f, 1);
        public static AnchorPoint BottomRight => new AnchorPoint(1, 1);

        public Vector2 Point { get; private set; }

        public AnchorPoint(float x, float y)
        {
            this.Point = new Vector2(x, y);
        }

        public static implicit operator Vector2(AnchorPoint ap)
        {
            return ap.Point;
        }
    }
}