using System;
using System.Threading.Tasks;
using Riptide;
using Src.GameplayView.ServerConnection;
using Src.NetworkingModule;
using Src.NetworkingModule.MessageCreators;

namespace Src.GameplayPresenter.ServerConnection
{
    public class ServerConnectionPresenter
    {
        private readonly IServerConnectionView _view;
        private readonly IClientWrapper _clientWrapper;
        private readonly IServerConnectionConfig _connectionConfig;
        private readonly IAsyncMessageCreator _initializationMessageCreator;

        public ServerConnectionPresenter(IServerConnectionView view, IClientWrapper clientWrapper, IServerConnectionConfig connectionConfig, IAsyncMessageCreator initializationMessageCreator)
        {
            _view = view;
            _clientWrapper = clientWrapper;
            _connectionConfig = connectionConfig;
            _initializationMessageCreator = initializationMessageCreator;
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


        public async Task TryConnectToServer()
        {
            _view.ShowConnectingMessage();
            var hostAddress = _connectionConfig.HostAddress;
            var maxConnectionAttempts = _connectionConfig.MaxConnectionAttempts;
            var initializationMessage = await _initializationMessageCreator.GetMessageAsync();
            _clientWrapper.Connect(hostAddress, maxConnectionAttempts, message: initializationMessage);
        }
        
    }
}