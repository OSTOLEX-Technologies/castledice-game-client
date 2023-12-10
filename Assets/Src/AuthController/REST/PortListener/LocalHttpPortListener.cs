using System;
using System.Net;
using System.Threading;

namespace Src.AuthController.REST.PortListener
{
    public class LocalHttpPortListener : ILocalHttpPortListener
    {
        private readonly IHttpPortListenerHandler _listenerHandler;
        private readonly HttpListener _listener;
        private Thread _listenerThread;
        private Action<string> _onCodeFetched;
        
        public LocalHttpPortListener(IHttpPortListenerHandler listenerHandler)
        {
            _listenerHandler = listenerHandler;
        }

        public void StartListening(Action<string> callback)
        {
            _onCodeFetched = callback;
            _listenerHandler.OnListenerFired += ListenerFired;
            _listenerHandler.Start();
        }

        public void StopListening()
        {
            _listenerHandler.Stop();
            _onCodeFetched = null;
        }

        private void ListenerFired(string result)
        {
            _onCodeFetched?.Invoke(result);
            StopListening();
        }
    }
}