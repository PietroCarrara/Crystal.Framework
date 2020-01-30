using System;

namespace Crystal.Framework.Media
{
    public interface IAudio : IDisposable
    {
        /// <summary>
        /// Plays the audio
        /// </summary>
        /// <param name="volume">Volume ranging from 0 to 1</param>
        /// <param name="panning">
        ///     How to split the audio between the speakers.
        ///     -1 for full left, 0 for center, 1 for full right.
        /// </param>
        void Play(float volume = 1, float panning = 0);
    }
}