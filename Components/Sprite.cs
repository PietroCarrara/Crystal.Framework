using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.Components
{
    public class Sprite : IComponent, ISprite, IDrawable
    {
        public readonly IDrawable Texture;

        public Vector2 Origin { get; set; } = new Vector2(.5f);

        public float Rotation { get; set; }

        public Vector2 Scale { get; set; } = new Vector2(1);

        public Vector2 Size
        {
            get => new Vector2(this.Texture.Width, this.Texture.Height) * this.Scale;
            set
            {
                this.Scale = new Vector2(
                    value.X / this.Texture.Width,
                    value.Y / this.Texture.Height
                );
            }   
        }

        public int Width => (int)this.Size.X;
        public int Height => (int)this.Size.Y;


        public Sprite(IDrawable texture, Vector2? origin = null)
        {
            this.Texture = texture;

            if (origin.HasValue)
            {
                this.Origin = origin.Value;
            }
        }

        public void Draw(Vector2 position, float delta, IDrawer spriteBatch)
        {
            spriteBatch.Draw(
                this.Texture,
                position,
                delta,
                this.Origin,
                this.Rotation,
                this.Scale,
                null
            );
        }

        public void Dispose()
        {
            this.Texture.Dispose();
        }
    }
}