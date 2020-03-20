using System.Collections.Generic;
using Crystal.Framework.Math;

namespace Crystal.Framework.Graphics
{
    /// <summary>
    /// A render target that can be resized
    /// </summary>
    public abstract class Canvas : IRenderTarget
    {
        public delegate void SizeChangedEventHandler(Canvas canvas, Point size);

        public event SizeChangedEventHandler SizeChanged;

        public bool Visible = true;
        
        public Point Size { get; private set; }

        public virtual void SetSize(Point size)
        {
            this.Size = size;

            SizeChanged?.Invoke(this, size);
        }

        public abstract void Dispose();

        public abstract void Clear();
    }
}