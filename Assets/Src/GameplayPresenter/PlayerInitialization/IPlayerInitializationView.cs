namespace Src.GameplayPresenter.PlayerInitialization
{
    public interface IPlayerInitializationView
    {
        public void ShowProcessMessage();
        public void HideProcessMessage();
        public void ShowFailureMessage();
    }
}