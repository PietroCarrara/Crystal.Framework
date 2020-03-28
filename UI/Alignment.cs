using System.Numerics;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    /// <summary>
    /// Represents the alignment some widget should follow.
    /// (0, 0) Means its top left should be aligned with the top left of its area
    /// (.5, .5) Means its center should be aligned with the center of its area
    /// (1, 1) Means its bottom right should be aligned with the bottom right of its area
    /// Use Apply() to calculate the position of a aligned object
    /// A calculation that reflects that is:
    ///     For a given widget WD, and an alignment AL, the top left of the aligned position is in the form:
    ///     WD.Area.TopLeft + WD.Area.Size * AL - (WD.width * AL.X, WD.height * AL.Y)
    /// </summary>
    public struct Alignment
    {
        public static Alignment TopLeft = new Alignment(0, 0);
        public static Alignment TopCenter = new Alignment(.5f, 0);
        public static Alignment TopRight = new Alignment(1, 0);
        public static Alignment CenterLeft = new Alignment(0, .5f);
        public static Alignment Center = new Alignment(.5f, .5f);
        public static Alignment CenterRight = new Alignment(1, .5f);
        public static Alignment BottomLeft = new Alignment(0, 1);
        public static Alignment BottomCenter = new Alignment(.5f, 1);
        public static Alignment BottomRight = new Alignment(1, 1);

        public float X, Y;

        public Alignment(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Positions a textureslice inside another textureslice
        /// </summary>
        /// <param name="availableArea">The area where we can position our texture slice</param>
        /// <param name="toPosition">The texture slice to position</param>
        /// <returns>The positioned texture slice</returns>
        public TextureSlice Apply(TextureSlice availableArea, TextureSlice toPosition)
        {
            var topLeft = availableArea.TopLeft + availableArea.Size * (Vector2)this
                          - new Vector2(toPosition.Width * this.X, toPosition.Height * this.Y);


            return new TextureSlice(
                (Point)topLeft,
                toPosition.Width,
                toPosition.Height
            );
        }

        public static implicit operator Vector2(Alignment alignment)
        {
            return new Vector2(alignment.X, alignment.Y);
        }

        public static implicit operator Alignment(Vector2 vector)
        {
            return new Alignment(vector.X, vector.Y);
        }
    }
}