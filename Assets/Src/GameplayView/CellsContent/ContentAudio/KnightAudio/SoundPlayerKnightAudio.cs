using Src.GameplayView.Audio;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    public class SoundPlayerKnightAudio : KnightAudio
    {
        private Sound _placeSound;
        private Sound _hitSound;
        private Sound _destroySound;
        private SoundPlayer _soundPlayer;

        public void Init(Sound placeSound, Sound hitSound, Sound destroySound, SoundPlayer soundPlayer)
        {
            _placeSound = placeSound;
            _hitSound = hitSound;
            _destroySound = destroySound;
            _soundPlayer = soundPlayer;
        }
        
        public override void PlayPlaceSound()
        {
            _soundPlayer.PlaySound(_placeSound);
        }

        public override void PlayHitSound()
        {
            _soundPlayer.PlaySound(_hitSound);
        }

        public override void PlayDestroySound()
        {
            _soundPlayer.PlaySound(_destroySound);
        }
    }
}