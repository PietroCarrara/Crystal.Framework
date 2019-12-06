using System;

namespace Crystal.Framework.Graphics
{
    public interface IDrawable : IDisposable
    {
        int Width { get; }
        int Height { get; }
    }
}