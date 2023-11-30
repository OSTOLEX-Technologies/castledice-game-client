namespace Src.GameplayView.Audio
{
    public class PrefabSoundPlayerFactory : ISoundPlayerFactory
    {
        private readonly SoundPlayer _prefab;
        private readonly IInstantiator _instantiator;

        public PrefabSoundPlayerFactory(SoundPlayer prefab, IInstantiator instantiator)
        {
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public SoundPlayer GetSoundPlayer()
        {
            return _instantiator.Instantiate(_prefab);
        }
    }
}