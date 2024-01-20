using System;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using UnityEngine;

namespace Tests.Utils.Mocks
{
    public class CastleAudioForTests : CastleAudio
    {
        public bool PlayHitSoundCalled { get; private set; }
        public bool PlayDestroySoundCalled { get; private set; }
        
        public override void PlayHitSound()
        {
            PlayHitSoundCalled = true;
        }

        public override void  PlayDestroySound()
        {
            PlayDestroySoundCalled = true;
            DestroySoundPlayed?.Invoke();
        }

        public override event Action DestroySoundPlayed;
    }
}