using System;

namespace Crystal.Framework.Graphics
{
    public interface IDrawable : IDisposable
    {
        Point Size => new Point(Width, Height);

        int Width { get; }
        int Height { get; }
    }
}