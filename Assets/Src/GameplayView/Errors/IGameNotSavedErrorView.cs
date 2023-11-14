namespace Src.GameplayView.Errors
{
    public interface IGameNotSavedErrorView
    {
        void ShowError(string message);
        void HideGameCreationProcessScreen();
    }
}