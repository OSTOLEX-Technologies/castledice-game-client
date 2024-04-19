using System;
using Riptide;

namespace Src.NetworkingModule
{
    public interface IClientWrapper : IMessageSender, IDisconnectedEventEmitter
    {
        bool Connect(string hostAddress, int maxConnectionAttempts = 5, byte messageHandlerGroupId = 0,
            Message message = null);
        
        bool IsConnected { get; }
        
        Client Client { get; }
        
        event EventHandler Connected;
        
        event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;
    }
}