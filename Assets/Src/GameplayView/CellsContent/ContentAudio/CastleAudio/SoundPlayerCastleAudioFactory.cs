using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentAudio.CastleAudio
{
    public class SoundPlayerCastleAudioFactory : ICastleAudioFactory
    {
        private readonly ICastleSoundsProvider _soundsProvider;
        private readonly SoundPlayerCastleAudio _prefab;
        private readonly IInstantiator _instantiator;

        public SoundPlayerCastleAudioFactory(ICastleSoundsProvider soundsProvider, SoundPlayerCastleAudio prefab, IInstantiator instantiator)
        {
            _soundsProvider = soundsProvider;
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public CastleAudio GetAudio(CastleGO castle)
        {
            var audio = _instantiator.Instantiate(_prefab);
            audio.Init(_soundsProvider.GetHitSound(castle), _soundsProvider.GetDestroySound(castle));
            return audio;
        }
    }
}