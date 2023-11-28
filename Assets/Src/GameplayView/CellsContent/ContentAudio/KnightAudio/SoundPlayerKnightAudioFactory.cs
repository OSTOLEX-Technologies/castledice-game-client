using castledice_game_logic.GameObjects;
using Src.GameplayView.Audio;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    public class SoundPlayerKnightAudioFactory : IKnightAudioFactory
    {
        private readonly IKnightSoundsProvider _soundsProvider;
        private readonly SoundPlayerKnightAudio _prefab;
        private readonly IInstantiator _instantiator;

        public SoundPlayerKnightAudioFactory(IKnightSoundsProvider soundsProvider, SoundPlayerKnightAudio prefab, IInstantiator instantiator)
        {
            _soundsProvider = soundsProvider;
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public KnightAudio GetAudio(Knight knight)
        {
            var placeSound = _soundsProvider.GetPlaceSound(knight);
            var hitSound = _soundsProvider.GetHitSound(knight);
            var destroySound = _soundsProvider.GetDestroySound(knight);
            var audio = _instantiator.Instantiate(_prefab);
            audio.Init(placeSound, hitSound, destroySound);
            return audio;
        }
    }
}