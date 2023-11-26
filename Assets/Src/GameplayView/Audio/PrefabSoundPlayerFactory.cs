namespace Src.GameplayView.Audio
{
    public class PrefabSoundPlayerFactory : IUnitySoundPlayerFactory
    {
        private readonly UnitySoundPlayer _prefab;
        private readonly IInstantiator _instantiator;

        public PrefabSoundPlayerFactory(UnitySoundPlayer prefab, IInstantiator instantiator)
        {
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public UnitySoundPlayer GetSoundPlayer()
        {
            return _instantiator.Instantiate(_prefab);
        }
    }
}