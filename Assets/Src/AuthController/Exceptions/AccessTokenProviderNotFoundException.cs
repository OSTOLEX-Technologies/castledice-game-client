using System;

namespace Src.AuthController.Exceptions
{
    [Serializable]
    public class AccessTokenProviderNotFoundException : Exception
    {
        public AuthType UsedAuthType { get; }
        
        public AccessTokenProviderNotFoundException() {}
        
        public AccessTokenProviderNotFoundException(AuthType usedAuthType)
        {
            UsedAuthType = usedAuthType;
        }
        
        public AccessTokenProviderNotFoundException(string message) : base(message)
        {}
        
        public AccessTokenProviderNotFoundException(string message, Exception inner) : base(message, inner)
        {}

        public AccessTokenProviderNotFoundException(string message, AuthType usedAuthType) : base(message)
        {
            UsedAuthType = usedAuthType;
        }
    }
}