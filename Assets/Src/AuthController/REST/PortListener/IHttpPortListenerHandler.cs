using System;

namespace Src.AuthController.REST.PortListener
{
    public interface IHttpPortListenerHandler
    {
        public event Action<string> OnListenerFired;
        
        public void Start();

        public void Stop();
    }
}