using System;
using Src.AuthController.REST;

namespace Src.AuthController.Exceptions
{
    [Serializable]
    public class RestRequestMethodNameNotFoundException : Exception
    {
        public RestRequestMethodType MethodType { get; }
        
        public RestRequestMethodNameNotFoundException() {}
        
        public RestRequestMethodNameNotFoundException(RestRequestMethodType methodType)
        {
            MethodType = methodType;
        }
        
        public RestRequestMethodNameNotFoundException(string message) : base(message)
        {}
        
        public RestRequestMethodNameNotFoundException(string message, Exception inner) : base(message, inner)
        {}

        public RestRequestMethodNameNotFoundException(string message, RestRequestMethodType methodType) : base(message)
        {
            MethodType = methodType;
        }
    }
}