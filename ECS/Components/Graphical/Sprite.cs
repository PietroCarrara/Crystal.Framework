using System;
using Crystal.Framework;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.ECS.Components.Graphical
{
    public class Sprite : IComponent, IDisposable
    {
        /// <summary>
        /// The texture of this sprite
        /// </summary>
        private ITexture texture;

        /// <summary>
        /// Point indicating the origin of the sprite
        /// X and Y must be in range [0, 1]
        /// (0, 0) means top left
        /// (1, 1) means bottom right
        /// </summary>
        public Vector2 Origin;

        /// <summary>
        /// The order in which to draw this sprite
        /// The less it is, the further back it is
        /// Should be in range [0, 1]
        /// </summary>
        public float Index = 0;

        /// <summary>
        /// Sprite rotarion in radians
        /// Flows on a clock-wise direction
        /// 0       >
        /// Pi/2    v
        /// Pi      <
        /// 3Pi/2   ^
        /// </summary>
        public float Rotation;

        public Vector2 Scale = new Vector2(1);

        public Sprite(ITexture texture, Vector2? origin = null)
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

        public void Draw(Vector2 position)
        {
            this.texture.Draw(
                position,
                this.Origin,
                this.Rotation,
                this.Scale,
                null, // TODO: SourceRect
                this.Index
            );
        }

        public void Dispose()
        {
            this.texture.Dispose();
        }
    }
}