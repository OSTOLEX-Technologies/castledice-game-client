using System;
using System.Net;

namespace Src.AuthController.Exceptions
{
    [Serializable]
    public class HttpClientRequestException : Exception
    {
        public HttpStatusCode RequestStatusCode { get; }
        
        public HttpClientRequestException() {}
        
        public HttpClientRequestException(HttpStatusCode requestStatusCode)
        {
            RequestStatusCode = requestStatusCode;
        }
        
        public HttpClientRequestException(string message) : base(message)
        {}
        
        public HttpClientRequestException(string message, Exception inner) : base(message, inner)
        {}

        public HttpClientRequestException(string message, HttpStatusCode requestStatusCode) : base(message)
        {
            RequestStatusCode = requestStatusCode;
        }
    }
}