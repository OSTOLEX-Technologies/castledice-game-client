using System;

namespace Src.Auth.REST.PortListener
{
    public class LocalHttpPortListener : ILocalHttpPortListener
    {
        private readonly IHttpPortListenerHandler _listenerHandler;
        private Action<string> _callback;

        public LocalHttpPortListener(IHttpPortListenerHandler listenerHandler)
        {
            _listenerHandler = listenerHandler;
        }

        public void StartListening(Action<string> callback)
        {
            _callback = callback;

            if (_listenerHandler is null) return;
            
            _listenerHandler.OnListenerFired += ListenerFired;
            _listenerHandler.Start();
        }

        public void Interrupt()
        {
            StopListening();
        }
        
        private void StopListening()
        {
            _callback = null;

            if (_listenerHandler is null) return;

            _listenerHandler.OnListenerFired -= ListenerFired;
            _listenerHandler.Dispose();
        }

        private void ListenerFired(string result)
        {
            _callback?.Invoke(result);
            StopListening();
        }
    }
}