using System;
using Src.AuthController.REST;

namespace Src.AuthController.Exceptions
{
    [Serializable]
    public class NotAssignedRestRequestMethodNameException : Exception
    {
        public RestRequestMethodType MethodType { get; }
        
        public NotAssignedRestRequestMethodNameException() {}
        
        public NotAssignedRestRequestMethodNameException(RestRequestMethodType methodType)
        {
            MethodType = methodType;
        }
        
        public NotAssignedRestRequestMethodNameException(string message) : base(message)
        {}
        
        public NotAssignedRestRequestMethodNameException(string message, Exception inner) : base(message, inner)
        {}

        public NotAssignedRestRequestMethodNameException(string message, RestRequestMethodType methodType) : base(message)
        {
            MethodType = methodType;
        }
    }
}