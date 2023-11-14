using UnityEngine;

namespace Src.GameplayView.Errors
{
    public class GameNotSavedErrorView : IGameNotSavedErrorView
    {
        private readonly IErrorPopup _errorPopup;
        private readonly GameObject _gameCreationProcessScreen;

        public GameNotSavedErrorView(IErrorPopup errorPopup, GameObject gameCreationProcessScreen)
        {
            _errorPopup = errorPopup;
            _gameCreationProcessScreen = gameCreationProcessScreen;
        }

        public void ShowError(string message)
        {
            _errorPopup.SetMessage(message);
            _errorPopup.Show();
        }

        public void HideGameCreationProcessScreen()
        {
            _gameCreationProcessScreen.SetActive(false);
        }
    }
}