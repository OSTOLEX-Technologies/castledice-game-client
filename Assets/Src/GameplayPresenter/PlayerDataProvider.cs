namespace Src.GameplayPresenter
{
    public abstract class PlayerDataProvider
    {
        public abstract string GetAccessToken();
        public abstract int GetId();
        public abstract bool IsAuthorized();
    }
}