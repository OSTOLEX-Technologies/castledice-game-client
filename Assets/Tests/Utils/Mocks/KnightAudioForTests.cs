using System;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;

namespace Tests.Utils.Mocks
{
    public class KnightAudioForTests : KnightAudio
    {
        public bool PlayPlaceSoundWasCalled { get; private set; }
        public bool PlayHitSoundWasCalled { get; private set; }
        public bool PlayDestroySoundWasCalled { get; private set; }


        public override void PlayPlaceSound()
        {
            PlayPlaceSoundWasCalled = true;
        }

        public override void PlayHitSound()
        {
            PlayHitSoundWasCalled = true;
        }

        public override void PlayDestroySound()
        {
            PlayDestroySoundWasCalled = true;
        }

        public override event Action DestroySoundPlayed;
    }
}