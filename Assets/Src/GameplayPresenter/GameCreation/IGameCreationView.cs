using System;
using Src.GameplayPresenter.GameCreation.GameSearching;

namespace Src.GameplayPresenter.GameCreation
{
    public interface IGameCreationView
    {
        public void ShowMatchmakingScreen();
        public void HideMatchmakingScreen();
        public void ShowCancellationScreen();
        public void HideCancellationScreen();
        public void ShowFail(SearchFailReason reason);

        public event Action CancelChosen;
        public event Action PlayChosen;
    }
}