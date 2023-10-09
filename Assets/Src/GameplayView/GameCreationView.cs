using System;
using JetBrains.Annotations;

namespace Src.GameplayView
{
    public abstract class GameCreationView
    {
        public abstract void ShowCreationProcessScreen();
        public abstract void HideCreationProcessScreen();
        public abstract void ShowCancelationMessage(string message);
        public abstract void HideCancelationMessage();
        public abstract void ShowNonAuthorizedMessage(string message);
        public abstract void HideNonAuthorizedMessage();

        public void ChooseCreateGame()
        {
            CreateGameChosen?.Invoke(this, EventArgs.Empty);
        }

        public void ChooseCancelGame()
        {
            CancelCreationChosen?.Invoke(this, EventArgs.Empty);
        }

        [CanBeNull] public event EventHandler CancelCreationChosen;
        [CanBeNull] public event EventHandler CreateGameChosen;
    }
}