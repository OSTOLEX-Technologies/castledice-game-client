using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Src.AuthController.REST
{
    public class HttpPortListener
    {
        private readonly HttpListener _listener;
        private Thread _listenerThread;
        private Action<string> _onCodeFetched;
        
        private const string ResponseHtml = "<h1>You can return to the app now!</h1>"; // TODO: Change this to a successful html response

        public HttpPortListener(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}/");
            _listener.Prefixes.Add($"http://127.0.0.1:{port}/");
            _listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        }

        public void StartListening(Action<string> callback)
        {
            _onCodeFetched = callback;
        
            _listener.Start();

            _listenerThread = new Thread(ListeningThread);
            _listenerThread.Start();
        }

        public void StopListening()
        {
            _listener.Stop();
            _onCodeFetched = null;
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

            if (!context.Request.QueryString.AllKeys.Contains("code")) return;
            UnityMainThreadDispatcher.Instance().Enqueue(() => _onCodeFetched?.Invoke(context.Request.QueryString.Get("code")));
        
            var buffer = Encoding.UTF8.GetBytes(ResponseHtml);

            context.Response.ContentLength64 = buffer.Length;
            var stream = context.Response.OutputStream;
            stream.Write(buffer, 0, buffer.Length);

            context.Response.Close(); 
        }
    }
}