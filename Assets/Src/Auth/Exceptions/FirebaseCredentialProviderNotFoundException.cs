using System;

namespace Src.Auth.Exceptions
{
    [Serializable]
    public class FirebaseCredentialProviderNotFoundException : Exception
    {
        public AuthType UsedAuthType { get; }
        
        public FirebaseCredentialProviderNotFoundException() {}
        
        public FirebaseCredentialProviderNotFoundException(AuthType usedAuthType)
        {
            UsedAuthType = usedAuthType;
        }
        
        public FirebaseCredentialProviderNotFoundException(string message) : base(message)
        {}
        
        public FirebaseCredentialProviderNotFoundException(string message, Exception inner) : base(message, inner)
        {}

        public FirebaseCredentialProviderNotFoundException(string message, AuthType usedAuthType) : base(message)
        {
            UsedAuthType = usedAuthType;
        }
    }
}