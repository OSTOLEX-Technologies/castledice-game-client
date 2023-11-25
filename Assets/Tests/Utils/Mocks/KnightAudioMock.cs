﻿using Src.GameplayView.CellsContent.ContentAudio;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using UnityEngine;

namespace Tests.Utils.Mocks
{
    public class KnightAudioMock : MonoBehaviour, IKnightAudio
    {
        public bool PlayPlaceSoundCalled { get; private set; }
        public bool PlayHitSoundCalled { get; private set; }
        public bool PlayDestroySoundCalled { get; private set; }

        public void PlayPlaceSound()
        {
            PlayPlaceSoundCalled = true;
        }

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