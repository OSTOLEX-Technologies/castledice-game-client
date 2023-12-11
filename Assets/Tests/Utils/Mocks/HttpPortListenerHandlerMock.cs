using System;
using Src.AuthController.REST.PortListener;

namespace Tests.Utils.Mocks
{
    public class HttpPortListenerHandlerMock : IHttpPortListenerHandler
    {
        private readonly string _sampleResult;
        public event Action<string> OnListenerFired;

        public HttpPortListenerHandlerMock(Action<string> callback, string sampleResult)
        {
            _sampleResult = sampleResult;
            OnListenerFired = callback;
        }
        public void Start()
        {
            OnListenerFired?.Invoke(_sampleResult);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}