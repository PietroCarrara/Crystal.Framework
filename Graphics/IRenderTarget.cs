using System;
namespace Crystal.Framework.Graphics
{
    /// <summary>
    /// Something that can be drawn to
    /// </summary>
    public interface IRenderTarget : IDisposable
    {
        // Removes any previously drawn objects
        void Clear();
    }
}