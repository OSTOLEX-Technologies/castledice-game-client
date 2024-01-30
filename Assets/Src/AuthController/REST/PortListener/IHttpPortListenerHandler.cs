using System;

namespace Src.AuthController.REST.PortListener
{
    public interface IHttpPortListenerHandler : IDisposable
    {
        public event Action<string> OnListenerFired;
        
        public void Start();
    }
}