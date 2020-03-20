using Crystal.Framework.Math;

namespace Crystal.Framework.Graphics
{
    /// <summary>
    /// An object that is aware of the screen size and can
    /// scale textures to fit on the screen
    /// </summary>
    public interface IScaler
    {
        /// <summary>
        /// Given the dimensions and position of a texture,
        /// returns the slice of the screen where it should be rendered
        /// </summary>
        /// <param name="texture">The dimensions and position of a texture</param>
        /// <returns>Where on the screen the texture should be rendered</returns>
        TextureSlice Scale(TextureSlice texture);

        /// <summary>
        /// Given the dimensions and position of a texture,
        /// return a transformation that translates coordinates from screen space
        /// to this texture's space
        /// </summary>
        /// <param name="texture">The dimensions and position of a texture</param>
        /// <returns>A transformation that translates screen space to texture space</returns>
        Matrix4 Invert(TextureSlice texture);
    }
}