using System;
using Riptide;
using Src.NetworkingModule;
using UnityEngine.UI;

namespace Src.GameplayPresenter.ServerConnection
{
    public class ConnectButtonHandler
    {
        private readonly Button _button;
        private readonly IServerConnectionPresenter _serverConnectionPresenter;
        private readonly IClientWrapper _clientWrapper;
        
        public ConnectButtonHandler(Button button, IServerConnectionPresenter serverConnectionPresenter, IClientWrapper clientWrapper)
        {
            _button = button;
            _serverConnectionPresenter = serverConnectionPresenter;
            _clientWrapper = clientWrapper;
            _button.onClick.AddListener(OnButtonClicked);
            _clientWrapper.Connected += OnConnected;
            _clientWrapper.ConnectionFailed += OnConnectionFailed;
            _clientWrapper.Disconnected += OnDisconnected;
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            _button.gameObject.SetActive(true);
        }

        private void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _button.gameObject.SetActive(true);
        }

        private void OnConnected(object sender, EventArgs e)
        {
            _button.gameObject.SetActive(false);
        }

        private void OnButtonClicked()
        {
            _serverConnectionPresenter.ConnectToServer();
        }
    }
}