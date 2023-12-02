using System;

namespace Src.AuthController.Exceptions
{
    [Serializable]
    public class NotSelectedAccessTokenProviderException : Exception
    {
        public AuthType UsedAuthType { get; }
        
        public NotSelectedAccessTokenProviderException() {}
        
        public NotSelectedAccessTokenProviderException(AuthType usedAuthType)
        {
            UsedAuthType = usedAuthType;
        }
        
        public NotSelectedAccessTokenProviderException(string message) : base(message)
        {}
        
        public NotSelectedAccessTokenProviderException(string message, Exception inner) : base(message, inner)
        {}

        public NotSelectedAccessTokenProviderException(string message, AuthType usedAuthType) : base(message)
        {
            UsedAuthType = usedAuthType;
        }
    }
}