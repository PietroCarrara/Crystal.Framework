namespace Crystal.Framework.Graphics
{
    public interface IAnimatable : IDrawable
    {
        /// <summary>
        /// Plays the animation at speed times the original speed
        /// </summary>
        /// <param name="speed">
        /// Speed to play (1 is normal, 2 is 2 times faster and so on).
        /// Should accept negative values to play backwards
        /// </param>
        void Play(float speed = 1);

        /// <summary>
        /// Holds the animation still in the current frame
        /// </summary>
        void Pause();

        /// <summary>
        /// Goes back to frame 0 (but doesn't stop playing)
        /// </summary>
        void Reset();

        /// <summary>
        /// Prepares the animation to be drawn
        /// </summary>
        void PreDraw(float delta);

        /// <summary>
        /// Returns wich part of the texture should be drawn
        /// </summary>
        TextureSlice GetSlice();
    }
}