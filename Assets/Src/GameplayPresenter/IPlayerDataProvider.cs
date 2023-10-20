namespace Src.GameplayPresenter
{
    public interface IPlayerDataProvider
    {
        string GetAccessToken();
        int GetId();
        bool IsAuthorized();
    }
}