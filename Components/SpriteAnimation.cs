using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.Components
{
    public class SpriteAnimation : ISpriteComponent, IAnimatable
    {
        public readonly IAnimatable Animation;

        public int Width => this.Animation.GetSourceRectangle().Width;

        public int Height => this.Animation.GetSourceRectangle().Height;

        public Vector2 Origin { get; set; } = new Vector2(.5f);
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public Vector2 Size
        {
            get => new Vector2(this.Animation.Width, this.Animation.Height) * this.Scale;
            set
            {
                this.Scale = new Vector2(
                    value.X / this.Animation.Width,
                    value.Y / this.Animation.Height
                );
            }
        }

        public SpriteAnimation(IAnimatable animation, Vector2? origin = null)
        {
            this.Animation = animation;
        }

        public SpriteAnimation(IDrawable texture,
                               Point indivudualSize,
                               float fps,
                               Vector2? scale = null,
                               int? numberOfSprites = null)
        {
            this.Animation = new SpriteSheetAnimation(texture,
                                                      indivudualSize,
                                                      fps,
                                                      numberOfSprites);
            this.Scale = scale.HasValue ? scale.Value : Vector2.One;
        }

        public void Draw(Vector2 position, float delta, IDrawer spriteBatch)
        {
            this.Animation.PreDraw(delta);

            spriteBatch.Draw(
                this.Animation,
                position,
                delta,
                this.Origin,
                this.Rotation,
                this.Scale,
                this.Animation.GetSourceRectangle()
            );
        }


        public void Pause()
        {
            this.Animation.Pause();
        }

        public void Play(float speed = 1)
        {
            this.Animation.Play(speed);
        }

        public void Reset()
        {
            this.Animation.Reset();
        }

        public void Dispose()
        {
            this.Animation.Dispose();
        }

        public void PreDraw(float delta)
        {
            this.Animation.PreDraw(delta);
        }

        public TextureSlice GetSourceRectangle()
        {
            return this.Animation.GetSourceRectangle();
        }
    }
}