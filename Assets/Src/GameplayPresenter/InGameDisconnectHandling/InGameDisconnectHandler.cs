using System;
using Riptide;
using Src.Components;
using Src.General.LoadingScenes;
using Src.NetworkingModule;
using UnityEngine;
using UnityEngine.UI;

namespace Src.GameplayPresenter.InGameDisconnectHandling
{
    public class InGameDisconnectHandler : IDisposable
    {
        private readonly IClientWrapper _clientWrapper;
        private readonly GameObject _disconnectedPopup;
        private readonly Button _returnToMenuButton;
        private readonly ISceneLoader _sceneLoader;
        private readonly SceneType _sceneType;

        public InGameDisconnectHandler(IClientWrapper clientWrapper, GameObject disconnectedPopup, Button returnToMenuButton, ISceneLoader sceneLoader, SceneType sceneType)
        {
            _clientWrapper = clientWrapper;
            _disconnectedPopup = disconnectedPopup;
            _returnToMenuButton = returnToMenuButton;
            _sceneLoader = sceneLoader;
            _sceneType = sceneType;
            _clientWrapper.Disconnected += OnDisconnected;
            _returnToMenuButton.onClick.AddListener(ReturnToMenuClicked);
        }

        private void ReturnToMenuClicked()
        {
            _sceneLoader.LoadSceneWithTransition(_sceneType);
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            _disconnectedPopup.SetActive(true);
        }

        public void Dispose()
        {
            _clientWrapper.Disconnected -= OnDisconnected;
        }
    }
}