using System;
using JetBrains.Annotations;

namespace Src.GameplayView.GameCreation
{
    public interface IGameCreationView
    {
        public void ShowCreationProcessScreen();
        public void HideCreationProcessScreen();
        public void ShowCancelationMessage(string message);
        public void HideCancelationMessage();
        public void ShowNonAuthorizedMessage(string message);
        public void HideNonAuthorizedMessage();

        /// <summary>
        /// This method should invoke <see cref="CreateGameChosen"/> event.
        /// </summary>
        public void ChooseCreateGame();

        /// <summary>
        /// This method should invoke <see cref="CancelCreationChosen"/> event.
        /// </summary>
        public void ChooseCancelGame();

        [CanBeNull] public event EventHandler CancelCreationChosen;
        [CanBeNull] public event EventHandler CreateGameChosen;
    }
}