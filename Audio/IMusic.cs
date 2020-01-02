using System;

namespace Crystal.Framework.Audio
{
    public interface IMusic
    {
        float Volume { get; set; }
        
        event Action<IMusic> PlaybackEnded;

        void Play();
        void Play(float volume);
    }
}