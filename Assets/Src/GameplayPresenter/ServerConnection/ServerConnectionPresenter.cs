using System;
using Riptide;
using Src.GameplayView.ServerConnection;
using Src.NetworkingModule;

namespace Src.GameplayPresenter.ServerConnection
{
    public class ServerConnectionPresenter : IServerConnectionPresenter
    {
        private readonly IServerConnectionView _view;
        private readonly IClientWrapper _clientWrapper;
        private readonly IServerConnectionConfig _connectionConfig;

        public ServerConnectionPresenter(IServerConnectionView view, IClientWrapper clientWrapper, IServerConnectionConfig connectionConfig)
        {
            _view = view;
            _clientWrapper = clientWrapper;
            _connectionConfig = connectionConfig;
            _clientWrapper.Connected += OnConnected;
            _clientWrapper.ConnectionFailed += OnConnectionFailed;
        }

        private void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _view.HideConnectingMessage();
            _view.ShowConnectionFailedMessage(e.Reason);
        }

        private void OnConnected(object sender, EventArgs e)
        {
            _view.HideConnectingMessage();
        }


        public void ConnectToServer()
        {
            if (_clientWrapper.IsConnected)
            {
                return;
            }
            _view.ShowConnectingMessage();
            var hostAddress = _connectionConfig.HostAddress;
            var maxConnectionAttempts = _connectionConfig.MaxConnectionAttempts;
            _clientWrapper.Connect(hostAddress, maxConnectionAttempts);
        }
        
    }
}