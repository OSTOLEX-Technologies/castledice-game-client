using System;
using System.Net;
using System.Threading;
using Src.AuthController.REST.PortListener.ListenerContextInterpretation;
using Src.AuthController.REST.PortListener.ListenerContextResponse;

namespace Src.AuthController.REST.PortListener
{
    public class HttpPortListenerHandler : IHttpPortListenerHandler
    {
        private readonly string _responseHtml;
        private readonly IHttpListenerContextInterpreter _listenerContextInterpreter;
        private readonly string _queryContextKey;
        private readonly IHttpListenerContextResponse _listenerContextResponse;
        private readonly HttpListener _listener;
        private Thread _listenerThread;

        public event Action<string> OnListenerFired;
        
        public HttpPortListenerHandler(int port, IHttpListenerContextInterpreter listenerContextInterpreter, string queryContextKey, IHttpListenerContextResponse listenerContextResponse, string responseHtml)
        {
            _responseHtml = responseHtml;
            _listenerContextInterpreter = listenerContextInterpreter;
            _queryContextKey = queryContextKey;
            _listenerContextResponse = listenerContextResponse;
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}/");
            _listener.Prefixes.Add($"http://127.0.0.1:{port}/");
            _listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        }
        
        public void Start()
        {
            _listener.Start();
            
            _listenerThread = new Thread(ListeningThread);
            _listenerThread.Start();
        }

        public void Stop()
        {
            _listener.Stop();
            OnListenerFired = null;
        }

        private void ListeningThread()
        {
            while (_listener.IsListening)
            {
                var result = _listener.BeginGetContext(ListenerCallback, _listener);
                result.AsyncWaitHandle.WaitOne();
            }
        }

        private void ListenerCallback(IAsyncResult result)
        {
            var context = _listener.EndGetContext(result);
            
            if (!_listenerContextInterpreter.Contains(context, _queryContextKey)) return;
            
            OnListenerFired?.Invoke(_listenerContextInterpreter.Get(context, _queryContextKey));

            _listenerContextResponse.SendResponse(context, _responseHtml);
        }
    }
}