using UnityEngine;

namespace Src.GameplayView.Audio
{
    public class Sound
    {
        public AudioClip Clip { get; }
        public float Volume { get; }
        
        public Sound(AudioClip clip, float volume)
        {
            Clip = clip;
            Volume = volume;
        }
    }
}