namespace Src.GameplayPresenter
{
    public interface IPlayerDataProvider
    {
        string GetAccessTokenAsync();
        int GetIdAsync();
        bool IsAuthorized();
    }
}