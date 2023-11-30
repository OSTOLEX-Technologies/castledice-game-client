using UnityEngine;

namespace Src.GameplayView.Audio
{
    public class AudioSourceSoundPlayer : SoundPlayer
    {
        [SerializeField] private AudioSource audioSource;
        
        public override void PlaySound(Sound sound)
        {
            audioSource.PlayOneShot(sound.Clip, sound.Volume);
        }
    }
}