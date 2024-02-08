using System;
using System.Net;
using System.Threading;
using Src.Auth.DeepLinking.Config;
using Src.Auth.DeepLinking.DeepLinkTextInjector;
using Src.Auth.REST.PortListener.ListenerContextInterpretation;
using Src.Auth.REST.PortListener.ListenerContextResponse;
using Src.Components;
using Src.TextAssetLoading;

namespace Src.Auth.REST.PortListener
{
    public class HttpPortListenerHandler : IHttpPortListenerHandler
    {
        private readonly IHttpListenerContextInterpreter _listenerContextInterpreter;
        private readonly string _queryContextKey;
        private readonly IHttpListenerContextResponse _listenerContextResponse;

        private readonly HttpListener _listener;
        private Thread _listenerThread;
        private HttpListenerContext _workingContext;

        public event Action<string> OnListenerFired;

        private readonly string _httpResponseTextFormatted;

        public HttpPortListenerHandler(
            int port, 
            IHttpListenerContextInterpreter listenerContextInterpreter, 
            string queryContextKey, 
            IHttpListenerContextResponse listenerContextResponse,
            TextAssetResourceLoader textAssetResourceLoader,
            IDeepLinkTextInjector textInjector)
        {
            _listenerContextInterpreter = listenerContextInterpreter;
            _queryContextKey = queryContextKey;
            _listenerContextResponse = listenerContextResponse;

            var httpResponseText = textAssetResourceLoader.GetAssetContent(TextAssetType.GoogleAuthResponsePage);
            _httpResponseTextFormatted = textInjector.InjectLink(
                httpResponseText, 
                DeepLinkConfig.GoogleAuthRedirectUri);
            
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

        public void Dispose()
        {
            _listener.Stop();
            _workingContext?.Response.Abort();
            
            _listener.Abort();
        }
        
        private async void ListeningThread()
        {
            if (!_listener.IsListening) return;
            
            _workingContext = await _listener.GetContextAsync();

            if (!_listenerContextInterpreter.Contains(_workingContext, _queryContextKey))
            {
                Dispose();
                return;
            }

            var responseContextKeyValue = _listenerContextInterpreter.Get(_workingContext, _queryContextKey);
            await _listenerContextResponse.SendResponse(_workingContext, _httpResponseTextFormatted);

            OnListenerFired?.Invoke(responseContextKeyValue);
        }
    }
}