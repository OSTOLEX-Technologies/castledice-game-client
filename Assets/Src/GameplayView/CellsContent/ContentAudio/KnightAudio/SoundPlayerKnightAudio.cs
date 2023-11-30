using Src.GameplayView.Audio;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    public class SoundPlayerKnightAudio : KnightAudio
    {
        [SerializeField] private SoundPlayer soundPlayer;
        private Sound _placeSound;
        private Sound _hitSound;
        private Sound _destroySound;

        public void Init(Sound placeSound, Sound hitSound, Sound destroySound)
        {
            _placeSound = placeSound;
            _hitSound = hitSound;
            _destroySound = destroySound;
        }
        
        public override void PlayPlaceSound()
        {
            soundPlayer.PlaySound(_placeSound);
        }

        public override void PlayHitSound()
        {
            soundPlayer.PlaySound(_hitSound);
        }

        public override void PlayDestroySound()
        {
            soundPlayer.PlaySound(_destroySound);
        }
    }
}