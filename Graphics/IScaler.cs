using System.Numerics;

namespace Crystal.Framework.Graphics
{
    /// <summary>
    /// An object that can reposition a rectangle (usually a texture)
    /// to fit inside another rectangle (usually a canvas)
    /// </summary>
    public interface IScaler
    {
        /// <summary>
        /// Fits a rectangle inside another
        /// </summary>
        /// <param name="container">The rectangle that will contain the other</param>
        /// <param name="fitting">The rectangle to be resized and </param>
        /// <returns>The shape that the secong parameter has to assume in order to fit it's container</returns>
        TextureSlice Scale(TextureSlice container, TextureSlice fitting);

        /// <summary>
        /// Creates a transformation that fits vectors in the second argument inside the first argument
        /// </summary>
        /// <param name="container">The rectangle that will contain the other</param>
        /// <param name="fitting">The rectangle to be resized and </param>
        /// <returns>The transformation for vectors to fit inside the container</returns>
        Matrix4x4 ScaleMatrix(TextureSlice container, TextureSlice fitting);
    }
}