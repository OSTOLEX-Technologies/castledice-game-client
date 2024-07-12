using System;
using Riptide;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.NetworkingModule;
using UnityEngine.UI;

namespace Src.GameplayPresenter.PlayerInitialization
{
    public class InitializeButtonHandler : IDisposable
    {
        private readonly IPlayerInitializationPresenter _presenter;
        private readonly IClientWrapper _clientWrapper;
        private readonly IPlayerInitializationResultEventsEmitter _eventsEmitter;
        private readonly Button _button;
        
        public InitializeButtonHandler(IPlayerInitializationPresenter presenter, IClientWrapper clientWrapper, IPlayerInitializationResultEventsEmitter eventsEmitter, Button button)
        {
            _presenter = presenter;
            _clientWrapper = clientWrapper;
            _eventsEmitter = eventsEmitter;
            _button = button;
            _button.onClick.AddListener(OnButtonClicked);
            _eventsEmitter.InitializationFailed += OnInitializationFailed;
            _eventsEmitter.InitializationSucceed += OnInitializationSucceed;
            _clientWrapper.Disconnected += OnDisconnected;
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            _button.gameObject.SetActive(false);
        }

        private void OnInitializationSucceed(object sender, EventArgs e)
        {
            _button.gameObject.SetActive(false);
        }

        private void OnInitializationFailed(object sender, EventArgs e)
        {
            _button.gameObject.SetActive(true);
        }

        private void OnButtonClicked()
        {
            _presenter.StartInitializationAsync();
            _button.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _eventsEmitter.InitializationSucceed -= OnInitializationSucceed;
            _eventsEmitter.InitializationFailed -= OnInitializationFailed;
            _clientWrapper.Disconnected -= OnDisconnected;
        }
    }
}