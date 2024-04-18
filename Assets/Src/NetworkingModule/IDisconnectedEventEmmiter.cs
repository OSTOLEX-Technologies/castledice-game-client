using System;
using Riptide;

namespace Src.NetworkingModule
{
    public interface IDisconnectedEventEmitter
    {
        event EventHandler<DisconnectedEventArgs> Disconnected;

    }
}