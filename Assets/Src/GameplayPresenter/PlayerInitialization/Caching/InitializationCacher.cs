using Src.General.PlayerInitialization;

namespace Src.GameplayPresenter.PlayerInitialization.Caching
{
    public class InitializationCacher : IPlayerInitializationSaver, IPlayerInitializationProvider
    {
        private static bool _initialized;
        
        public void SetInitialization(bool initialized)
        {
            Initialized = initialized;
        }

        public bool Initialized
        {
            get => _initialized;
            private set => _initialized = value;
        }
    }
}