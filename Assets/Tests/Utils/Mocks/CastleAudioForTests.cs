using Src.GameplayView.CellsContent.ContentAudio.TreeAudio;
using UnityEngine;

namespace Tests.Utils.Mocks
{
    public class CastleAudioForTests : MonoBehaviour, ICastleAudio
    {
        public bool PlayHitSoundCalled { get; private set; }
        public bool PlayDestroySoundCalled { get; private set; }
        
        public void PlayHitSound()
        {
            PlayHitSoundCalled = true;
        }

        public void PlayDestroySound()
        {
            PlayDestroySoundCalled = true;
        }
    }
}