using System;

namespace Crystal.Framework.Media
{
    public interface IMusic
    {
        float Volume { get; set; }
        
        event Action<IMusic> PlaybackEnded;

        void Play();
        void Play(float volume);
    }
}