using Src.GameplayView.CellsContent.ContentAudio;
using UnityEngine;

namespace Tests.Utils.Mocks
{
    public class AudioClipPlayerForTests : MonoBehaviour, IAudioClipPlayer
    {
        public AudioClip LastPlayedClip { get; private set; }
        
        public void PlayClip(AudioClip clip)
        {
            LastPlayedClip = clip;
        }
    }
}