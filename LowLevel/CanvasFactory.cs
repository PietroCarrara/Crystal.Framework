using Crystal.Framework.Graphics;

namespace Crystal.Framework.LowLevel
{
    public abstract class CanvasFactory
    {
        internal static CanvasFactory Instance;

        /// <summary>
        /// Creates a canvas that is the size of the window and resizes when the window does
        /// </summary>
        public abstract Canvas Create();

        /// <summary>
        /// Creates a canvas of the specified size
        /// </summary>
        public abstract Canvas Create(Point size);
    }
}