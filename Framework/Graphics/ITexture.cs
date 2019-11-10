using System;

namespace Crystal.Framework.Graphics
{
    public interface ITexture : IDisposable
    {
        int Width { get; }
        int Height { get; }
        
        /// <summary>
        /// Draw this texture
        /// </summary>
        /// <param name="position">Where to draw this texture</param>
        /// <param name="origin">
        ///     This texture's origin. X and Y should be in range
        ///     [-0, 1]. (0, 0) means top left, (1, 1) means bottom right
        ///     Defaults to 0.5, 0.5
        /// </param>
        /// <param name="rotation">
        ///     The clockwise rotation in radians of this texture.
        /// </param>
        /// <param name="scale">
        ///     The X and Y scale of this texture.
        ///     -1 should yield a inverted texture,
        ///     mirrored on the origin
        /// </param>
        /// <param name="sourceRectangle">
        ///     The "slice" of this texture to draw.
        ///     If null, draw the whole sprite
        /// </param>
        /// <param name="layerDepth">
        ///     The layer this texture should be drawn.
        ///     Should be in range [0, 1]. 1 means front,
        ///     0 means back.
        /// </param>
        void Draw(
            Vector2 position,
            Vector2? origin = null,
            float rotation = 0,
            Vector2? scale = null,
            TextureSlice? sourceRectangle = null,
            // Color color,
            float layerDepth = 1
        );
    }
}