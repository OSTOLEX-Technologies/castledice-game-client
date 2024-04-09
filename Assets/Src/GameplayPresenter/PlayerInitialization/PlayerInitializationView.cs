using UnityEngine;

namespace Src.GameplayPresenter.PlayerInitialization
{
    public class PlayerInitializationView
    {
        private readonly GameObject _processMessage;
        private readonly GameObject _failureMessage;
        
        public PlayerInitializationView(GameObject processMessage, GameObject failureMessage)
        {
            _processMessage = processMessage;
            _failureMessage = failureMessage;
        }

        public void ShowProcessMessage()
        {
            _processMessage.SetActive(true);
        }

        public void HideProcessMessage()
        {
            _processMessage.SetActive(false);
        }

        public void ShowFailureMessage()
        {
            _failureMessage.SetActive(true);
        }
    }
}