using System;

namespace Crystal.Framework.Graphics
{
    /// <summary>
    /// Something that can be drawn to.
    /// It's size may dinamically change.
    /// </summary>
    public interface IRenderTarget : IDisposable
    {
        public delegate void SizeChangedEventHandler(IResizeableRenderTarget canvas, Point size);

        public event SizeChangedEventHandler SizeChanged;

        /// <summary>
        /// The size of this render target
        /// </summary>
        Point Size { get; }

        // Removes any previously drawn objects
        void Clear(Color color);
    }
}