namespace Crystal.Framework.Graphics
{
    /// <summary>
    /// A render target that can be resized
    /// </summary>
    public abstract class Canvas : IRenderTarget
    {
        public abstract void SetSize(Point size);

        public abstract void Dispose();
    }
}