using System;
using Riptide;

namespace Src.NetworkingModule
{
    public class ClientWrapper : IClientWrapper
    {
        public Client Client { get; private set; }
        
        public event EventHandler<DisconnectedEventArgs> Disconnected;
        
        public event EventHandler Connected;
        
        public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;

        public ClientWrapper(Client client)
        {
            Client = client;
            Client.Connected += OnConnected;
            Client.Disconnected += OnDisconnected;
            Client.ConnectionFailed += OnConnectionFailed;
        }
         
        public void Send(Message message)
        {
            Client.Send(message);
        }

        public bool Connect(string hostAddress, int maxConnectionAttempts = 5, byte messageHandlerGroupId = 0, Message message = null)
        {
            return Client.Connect(hostAddress, maxConnectionAttempts, messageHandlerGroupId, message);
        }
        
        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }
        
        private void OnConnected(object sender, EventArgs e)
        {
            Connected?.Invoke(this, e);
        }
        
        private void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            ConnectionFailed?.Invoke(this, e);
        }
        
        ~ClientWrapper()
        {
            Client.Connected -= OnConnected;
            Client.Disconnected -= OnDisconnected;
            Client.ConnectionFailed -= OnConnectionFailed;
        }
    }
}