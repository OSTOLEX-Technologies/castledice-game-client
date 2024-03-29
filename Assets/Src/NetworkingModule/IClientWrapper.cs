using System;
using Riptide;

namespace Src.NetworkingModule
{
    public interface IClientWrapper : IMessageSender
    {
        bool Connect(string hostAddress, int maxConnectionAttempts = 5, byte messageHandlerGroupId = 0,
            Message message = null);
        
        bool IsConnected { get; }
        
        Client Client { get; }
        
        event EventHandler<DisconnectedEventArgs> Disconnected;
        
        event EventHandler Connected;
        
        event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;
    }
}