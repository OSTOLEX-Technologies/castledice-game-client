using Src.General.PlayerInitialization;

namespace Src.GameplayPresenter.PlayerInitialization.Caching
{
    public class InitializationCacher : IPlayerInitializationSaver, IPlayerInitializationProvider
    {
        public void SetInitialization(bool initialized)
        {
            Initialized = initialized;
        }

        public bool Initialized { get; private set; }
    }
}