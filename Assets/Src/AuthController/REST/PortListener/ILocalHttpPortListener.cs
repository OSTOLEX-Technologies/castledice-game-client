using System;

namespace Src.AuthController.REST.PortListener
{
    public interface ILocalHttpPortListener
    {
        public void StartListening(Action<string> callback);
        
        public void StopListening();
    }
}