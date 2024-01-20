using System;
using Src.GameplayView.Audio;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.CastleAudio
{
    public class SoundPlayerCastleAudio : CastleAudio
    {
        [SerializeField] private SoundPlayer soundPlayer;
        private Sound _hitSound;
        private Sound _destroySound;
        
        public void Init(Sound hitSound, Sound destroySound)
        {
            _hitSound = hitSound;
            _destroySound = destroySound;
        }
        
        public override void PlayHitSound()
        {
            soundPlayer.PlaySound(_hitSound);
        }

        public override void PlayDestroySound()
        {
            soundPlayer.PlaySound(_destroySound);
            Invoke(nameof(FireDestroySoundPlayed), _destroySound.Clip.length);
        }

        private void FireDestroySoundPlayed()
        {
            DestroySoundPlayed?.Invoke();
        }

        public override event Action DestroySoundPlayed;
    }
}