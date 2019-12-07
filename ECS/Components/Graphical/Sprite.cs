using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.ECS.Components.Graphical
{
    public class Sprite : IComponent, IDisposable
    {
        /// <summary>
        /// The texture of this sprite
        /// </summary>
        private IDrawable texture;

        /// <summary>
        /// Point indicating the origin of the sprite
        /// X and Y must be in range [0, 1]
        /// (0, 0) means top left
        /// (1, 1) means bottom right
        /// </summary>
        public Vector2 Origin;

        /// <summary>
        /// Sprite rotarion in radians
        /// Flows on a clock-wise direction
        /// 0       >
        /// Pi/2    v
        /// Pi      <
        /// 3Pi/2   ^
        /// </summary>
        public float Rotation;

        public float Width => texture.Width * Scale.X;
        public float Height => texture.Height * Scale.Y;

        public Vector2 Scale = new Vector2(1);

        public Sprite(IDrawable texture, Vector2? origin = null)
        {
            this.texture = texture;

            if (origin.HasValue)
            {
                this.Origin = origin.Value;
            }
            else
            {
                // Centered origin
                this.Origin = new Vector2(.5f);
            }
        }

        public void Draw(Vector2 position, IDrawer spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                position,
                this.Origin,
                this.Rotation,
                this.Scale,
                null // TODO: SourceRect
            );
        }

        public void Dispose()
        {
            this.texture.Dispose();
        }
    }
}