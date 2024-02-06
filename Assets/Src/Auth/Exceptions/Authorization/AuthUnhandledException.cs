using System;

namespace Src.Auth.Exceptions.Authorization
{
    [Serializable]
    public class AuthUnhandledException : Exception
    {
        
        public AuthUnhandledException() {}

        public AuthUnhandledException(string message) : base(message)
        {}
        
        public AuthUnhandledException(string message, Exception inner) : base(message, inner)
        {}
    }
}