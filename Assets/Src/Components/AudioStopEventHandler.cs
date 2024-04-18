using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Components
{
    public class AudioStopEventHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private UnityEvent onAudioStop;

        public Action<AudioClip> AudioPlaybackEnded;
        public bool Invoked { get; private set; }

        private void Awake()
        {
            if (audioSource.playOnAwake)
            {
                WaitUntilAudioEnds();
            }
        }

        public void PlayAudio()
        {
            audioSource.Play();
            WaitUntilAudioEnds();
        }

        private async Task WaitUntilAudioEnds()
        {
            await Task.Delay((int)(audioSource.clip.length * 1000));
            onAudioStop?.Invoke();
            AudioPlaybackEnded?.Invoke(audioSource.clip);
            Invoked = true;
        }
    }
}
