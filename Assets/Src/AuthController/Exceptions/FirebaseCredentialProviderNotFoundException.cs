using System;

namespace Src.AuthController.Exceptions
{
    [Serializable]
    public class FirebaseCredentialProviderNotFoundException : Exception
    {
        public FirebaseAuthProviderType UsedAuthType { get; }
        
        public FirebaseCredentialProviderNotFoundException() {}
        
        public FirebaseCredentialProviderNotFoundException(FirebaseAuthProviderType usedAuthType)
        {
            UsedAuthType = usedAuthType;
        }
        
        public FirebaseCredentialProviderNotFoundException(string message) : base(message)
        {}
        
        public FirebaseCredentialProviderNotFoundException(string message, Exception inner) : base(message, inner)
        {}

        public FirebaseCredentialProviderNotFoundException(string message, FirebaseAuthProviderType usedAuthType) : base(message)
        {
            UsedAuthType = usedAuthType;
        }
    }
}