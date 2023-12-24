using System;
using System.Threading;

namespace Src.AuthController.REST.PortListener
{
    public class LocalHttpPortListener : ILocalHttpPortListener
    {
        private readonly IHttpPortListenerHandler _listenerHandler;
        private Thread _listenerThread;
        private Action<string> _callback;
        
        public LocalHttpPortListener(IHttpPortListenerHandler listenerHandler)
        {
            _listenerHandler = listenerHandler;
        }

        ~LocalHttpPortListener()
        {
            _callback = null;
        }

        public void StartListening(Action<string> callback)
        {
            _callback = callback;
            _listenerHandler.OnListenerFired += ListenerFired;
            _listenerHandler.Start();
        }

        public void StopListening()
        {
            _listenerHandler.OnListenerFired -= ListenerFired;
            _listenerHandler.Stop();
            _callback = null;
        }

        private void ListenerFired(string result)
        {
            _callback?.Invoke(result);
            StopListening();
        }
    }
}