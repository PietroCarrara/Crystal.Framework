using Crystal.Framework.Math;
using Crystal.Framework.UI;

namespace Crystal.Framework.Graphics
{
    public interface IDrawer
    {
        /// <summary>
        /// Starts drawing on the given viewport
        /// </summary>
        /// <param name="screen">The viewport to draw on. If null, uses the whole screen</param>
        /// <param name="transformMatrix">The matrix used to apply transforms</param>
        /// <param name="samplerState">What strategy to use when upscaling or downscaling the textures</param>
        void BeginDraw(
            TextureSlice? viewport = null,
            Matrix4 transformMatrix = null,
            SamplerState samplerState = SamplerState.LinearClamp
        );

        /// <summary>
        /// Ends the drawing cycle and flushes the textures to the screen
        /// </summary>
        void EndDraw();

        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">The texture to be drawn</param>
        /// <param name="position">Where to draw the texture</param>
        /// <param name="deltaTime">Time elapsed sice the last frame</param>
        /// <param name="origin">
        ///     The texture's origin. X and Y should be in range
        ///     [0, 1]. (0, 0) means top left, (1, 1) means bottom right
        ///     Should default to 0.5, 0.5
        /// </param>
        /// <param name="rotation">
        ///     The clockwise rotation in radians of the texture.
        /// </param>
        /// <param name="scale">
        ///     The X and Y scale of the texture.
        ///     -1 should yield a inverted texture,
        ///     mirrored on the origin
        /// </param>
        /// <param name="sourceRectangle">
        ///     The "slice" of the texture to draw.
        ///     If null, draw the whole sprite
        /// </param>
        void Draw(
            IDrawable texture,
            Vector2 position,
            float deltaTime,
            Vector2? origin = null,
            float rotation = 0,
            Vector2? scale = null,
            TextureSlice? sourceRectangle = null
            // TODO: Color color
        );


        /// <summary>
        /// Draw a texture
        /// </summary>
        /// <param name="texture">The texture to be drawn</param>
        /// <param name="destinationRectangle">Where to draw the texture</param>
        /// <param name="deltaTime">Time elapsed sice the last frame</param>
        /// <param name="origin">
        ///     The texture's origin. X and Y should be in range
        ///     [0, 1]. (0, 0) means top left, (1, 1) means bottom right
        ///     Should default to 0.5, 0.5
        /// </param>
        /// <param name="rotation">
        ///     The clockwise rotation in radians of the texture.
        /// </param>
        /// <param name="sourceRectangle">
        ///     The "slice" of the texture to draw.
        ///     If null, draw the whole sprite
        /// </param>
        void Draw(
            IDrawable texture,
            Rectangle destinationRectangle,
            float deltaTime,
            Vector2? origin = null,
            float rotation = 0,
            TextureSlice? sourceRectangle = null
            // TODO: Color color
        );

        /// <summary>
        /// Draw a string
        /// </summary>
        /// <param name="font">The font to use</param>
        /// <param name="position">Where to position the string</param>
        /// <param name="text">The text to draw</param>
        /// <param name="rotation">Clockwise rotation in radians</param>
        void DrawString(
            IFont font,
            Vector2 position,
            string text,
            float rotation = 0
        );
    }
}