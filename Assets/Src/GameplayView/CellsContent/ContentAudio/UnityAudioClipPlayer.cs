using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio
{
    public class UnityAudioClipPlayer : MonoBehaviour, IAudioClipPlayer
    {
        [SerializeField] private AudioSource audioSource;
        
        public void PlayClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}