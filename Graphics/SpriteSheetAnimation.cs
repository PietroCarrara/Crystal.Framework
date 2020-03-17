namespace Crystal.Framework.Graphics
{
    public class SpriteSheetAnimation : IAnimatable
    {
        public int Width => this.indivudualSize.X;
        public int Height => this.indivudualSize.Y;

        public readonly IDrawable Texture;

        private readonly Point indivudualSize;
        private readonly float secondsPerFrame;
        private readonly int numberOfSprites;
        private readonly int totalColumns;
        private readonly int totalRows;

        private TextureSlice currentFrame;
        private float playingSpeed = 0;

        private float timer = 0;

        // The row and column of the current
        // sprite
        private Point currentSprite;

        public SpriteSheetAnimation(IDrawable texture,
                                    Point indivudualSize,
                                    float fps,
                                    int? numberOfSprites = null)
        {
            this.Texture = texture;
            this.indivudualSize = indivudualSize;
            this.secondsPerFrame = 1 / fps;

            this.totalColumns = texture.Width / this.indivudualSize.X;
            this.totalRows = texture.Height / this.indivudualSize.Y;

            if (numberOfSprites.HasValue)
            {
                this.numberOfSprites = numberOfSprites.Value;
            }
            else
            {
                this.numberOfSprites = this.totalColumns * this.totalRows;
            }

            this.Reset();
            this.Play();
        }

        public void Dispose()
        {
            this.Texture.Dispose();
        }

        public TextureSlice GetSourceRectangle()
        {
            return this.currentFrame;
        }

        public void Pause()
        {
            this.playingSpeed = 0;
        }

        public void Play(float speed = 1)
        {
            this.playingSpeed = speed;
        }

        public void PreDraw(float delta)
        {
            this.timer += delta * playingSpeed;

            if (this.timer > this.secondsPerFrame)
            {
                this.timer = 0;
                this.advanceFrame();
            }
        }

        /// <summary>
        /// Advances a frame in the animation.
        /// Goes left to right, top to bottom
        /// </summary>
        private void advanceFrame()
        {
            this.currentSprite.X++;

            if (this.currentSprite.X >= this.totalColumns)
            {
                this.currentSprite.X = 0;
                this.currentSprite.Y++;
            }

            if (this.currentSprite.Y >= this.totalRows)
            {
                this.currentSprite.Y = 0;
            }

            this.recalcCurrentFrame();
        }

        private void recalcCurrentFrame()
        {
            this.currentFrame = new TextureSlice(
                this.currentSprite.X * indivudualSize.X,
                this.currentSprite.Y * indivudualSize.Y,
                this.indivudualSize.X,
                this.indivudualSize.Y
            );
        }

        public void Reset()
        {
            this.currentSprite.X = 0;
            this.currentSprite.Y = 0;
            
            this.recalcCurrentFrame();
        }
    }
}