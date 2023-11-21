using Src.GameplayView.Errors;

namespace Src.GameplayPresenter.Errors
{
    public class GameNotSavedErrorPresenter : IErrorPresenter
    {
        private readonly IGameNotSavedErrorView _view;

        public GameNotSavedErrorPresenter(IGameNotSavedErrorView view)
        {
            _view = view;
        }
        public void ShowError(string message)
        {
            _view.HideGameCreationProcessScreen();
            _view.ShowError(message);
        }
    }
}