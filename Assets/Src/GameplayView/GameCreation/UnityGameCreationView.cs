using System;
using UnityEngine;

namespace Src.GameplayView.GameCreation
{
    //TODO: Finish this class and test
    public class UnityGameCreationView : MonoBehaviour, IGameCreationView
    {
        [SerializeField] private GameObject creationProcessScreen;
        
        public void ShowCreationProcessScreen()
        {
            creationProcessScreen.SetActive(true);
        }

        public void HideCreationProcessScreen() 
        {
            creationProcessScreen.SetActive(false);
        }

        public void ShowCancelationMessage(string message)
        {

        }

        public void HideCancelationMessage()
        {

        }

        public void ShowNonAuthorizedMessage(string message)
        {

        }

        public void HideNonAuthorizedMessage()
        {
            
        }

        public void ChooseCreateGame()
        {
            CreateGameChosen?.Invoke(this, EventArgs.Empty);
        }

        public void ChooseCancelGame()
        {
            CancelCreationChosen?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CancelCreationChosen;
        public event EventHandler CreateGameChosen;
    }
}