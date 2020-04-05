using System.Numerics;
using System;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.Components
{
    public class SpriteAnimation : Sprite
    {
        public readonly IAnimatable Animation;

        public override Vector2 Size
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

        public SpriteAnimation(IAnimatable animation, Vector2? origin = null) : base(animation, origin)
        {
            this.Animation = animation;
        }

        public SpriteAnimation(IDrawable texture,
                               Point indivudualSize,
                               float fps,
                               Vector2? scale = null,
                               int? numberOfSprites = null) : base(texture, null)
        {
            this.Animation = new SpriteSheetAnimation(texture,
                                                      indivudualSize,
                                                      fps,
                                                      numberOfSprites);
            this.Scale = scale.HasValue ? scale.Value : Vector2.One;
        }

        public override void Draw(Vector2 position, float delta, IDrawer spriteBatch)
        {
            this.Animation.PreDraw(delta);

            spriteBatch.Draw(
                this.Animation,
                position,
                this.Tint,
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

        public override void Dispose()
        {
            base.Dispose();

            this.Animation.Dispose();
        }

        public void PreDraw(float delta)
        {
            this.Animation.PreDraw(delta);
        }
    }
}