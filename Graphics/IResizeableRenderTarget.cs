namespace Crystal.Framework.Graphics
{
    /// <summary>
    /// A render target that can be resized
    /// </summary>
    public interface IResizeableRenderTarget : IRenderTarget
    {
        void SetSize(Point size);
    }
}