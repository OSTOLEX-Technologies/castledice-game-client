using System;

namespace Src.Auth.REST.PortListener
{
    public interface ILocalHttpPortListener
    {
        public void StartListening(Action<string> callback);
    }
}