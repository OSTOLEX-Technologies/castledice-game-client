using System;

namespace Src.AuthController.Exceptions.Authorization
{
    [Serializable]
    public class AuthFailedException : Exception
    {
        public string Reason { get; }
        
        public AuthFailedException() {}

        public AuthFailedException(string message) : base(message)
        {}
        
        public AuthFailedException(string message, Exception inner) : base(message, inner)
        {}

        public AuthFailedException(string message, string reason) : base(message)
        {
            Reason = reason;
        }
    }
}