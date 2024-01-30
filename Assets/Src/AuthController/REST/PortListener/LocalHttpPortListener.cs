using System;

namespace Src.AuthController.REST.PortListener
{
    public class LocalHttpPortListener : ILocalHttpPortListener
    {
        //public static LocalHttpPortListener Instance { get; private set; }
        
        private IHttpPortListenerHandler _listenerHandler;
        private Action<string> _callback;

        public LocalHttpPortListener(IHttpPortListenerHandler listenerHandler)
        {
            _listenerHandler = listenerHandler;
        }

        // private void Awake()
        // {
        //     if (Instance != null)
        //     {
        //         return;
        //     }
        //
        //     Instance = this;
        //
        //     _listenerHandler = new HttpPortListenerHandler(
        //         GoogleAuthConfig.LoopbackPort,
        //         new HttpListenerContextInterpreter(),
        //         GoogleAuthConfig.AuthCodeQueryKeyName,
        //         new HttpListenerContextResponse(),
        //         GoogleAuthConfig.ResponseHtml);
        // }

        public void StartListening(Action<string> callback)
        {
            _callback = callback;

            if (_listenerHandler is null) return;
            
            _listenerHandler.OnListenerFired += ListenerFired;
            _listenerHandler.Start();
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