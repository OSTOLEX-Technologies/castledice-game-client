using System;
using Src.AuthController.REST.PortListener;

namespace Tests.Utils.Mocks
{
    public class HttpPortListenerHandlerMock : IHttpPortListenerHandler
    { 
        public event Action<string> OnListenerFired;

        public void Start()
        {
            OnListenerFired?.Invoke("");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}