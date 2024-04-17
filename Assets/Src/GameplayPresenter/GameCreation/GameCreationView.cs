using System;
using Src.GameplayPresenter.GameCreation.GameSearching;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameCreationView : IGameCreationView
    {
        private readonly Button _playButton;
        private readonly Button _cancelButton;
        private readonly GameObject _matchmakingScreen;
        private readonly GameObject _cancellationScreen;
        private readonly GameObject _failMessage;
        private readonly TextMeshProUGUI _failMessageTextMesh;
        private readonly IFailMessagesConfig _failMessagesConfig;

        public GameCreationView(Button playButton, 
            Button cancelButton, 
            GameObject matchmakingScreen, 
            GameObject cancellationScreen, 
            GameObject failMessage, 
            TextMeshProUGUI failMessageTextMesh, 
            IFailMessagesConfig failMessagesConfig)
        {
            _matchmakingScreen = matchmakingScreen;
            _cancellationScreen = cancellationScreen;
            _failMessage = failMessage;
            _failMessageTextMesh = failMessageTextMesh;
            _failMessagesConfig = failMessagesConfig;
            _playButton = playButton;
            _playButton.onClick.AddListener(() => PlayChosen?.Invoke());
            _cancelButton = cancelButton;
            _cancelButton.onClick.AddListener(() => CancelChosen?.Invoke());
        }
        
        public void ShowMatchmakingScreen()
        {
            _matchmakingScreen.SetActive(true);
        }

        public void HideMatchmakingScreen()
        {
            _matchmakingScreen.SetActive(false);
        }

        public void ShowCancellationScreen()
        {
            _cancellationScreen.SetActive(true);
        }

        public void HideCancellationScreen()
        {
            _cancellationScreen.SetActive(false);
        }

        public void ShowFail(SearchFailReason reason)
        {
            _failMessage.SetActive(true);
            _failMessageTextMesh.text = _failMessagesConfig.GetFailMessage(reason);
        }

        public event Action CancelChosen;
        public event Action PlayChosen;
    }
}