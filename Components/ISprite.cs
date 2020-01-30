using Crystal.Framework.Graphics;

namespace Crystal.Framework.Components
{
    public interface ISprite : IComponent
    {
        /// <summary>
        /// Point indicating the origin of the sprite
        /// X and Y must be in range [0, 1]
        /// (0, 0) means top left
        /// (1, 1) means bottom right
        /// </summary>
        Vector2 Origin { get; set; }

        /// <summary>
        /// Sprite rotarion in radians
        /// Flows on a clock-wise direction
        /// 0       >
        /// Pi/2    v
        /// Pi      <
        /// 3Pi/2   ^
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// The scale in the X and Y axis
        /// </summary>
        Vector2 Scale { get; set; }

        /// <summary>
        /// The original sprite size times the scale
        /// </summary>
        Vector2 Size { get; set; }

        /// <summary>
        /// Draws this sprite
        /// </summary>
        /// <param name="position">Where to draw</param>
        /// <param name="delta">Delta Time</param>
        /// <param name="spriteBatch">What to use when drawing</param>
        void Draw(Vector2 position, float delta, IDrawer spriteBatch);
    }
}