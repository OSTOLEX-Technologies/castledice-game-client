using System;

namespace Src.Auth.REST.PortListener
{
    public interface IHttpPortListenerHandler : IDisposable
    {
        public event Action<string> OnListenerFired;
        
        public void Start();
    }
}