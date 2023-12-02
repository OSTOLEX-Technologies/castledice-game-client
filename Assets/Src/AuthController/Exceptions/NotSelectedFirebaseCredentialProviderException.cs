using System;

namespace Src.AuthController.Exceptions
{
    [Serializable]
    public class NotSelectedFirebaseCredentialProviderException : Exception
    {
        public FirebaseAuthProviderType UsedAuthType { get; }
        
        public NotSelectedFirebaseCredentialProviderException() {}
        
        public NotSelectedFirebaseCredentialProviderException(FirebaseAuthProviderType usedAuthType)
        {
            UsedAuthType = usedAuthType;
        }
        
        public NotSelectedFirebaseCredentialProviderException(string message) : base(message)
        {}
        
        public NotSelectedFirebaseCredentialProviderException(string message, Exception inner) : base(message, inner)
        {}

        public NotSelectedFirebaseCredentialProviderException(string message, FirebaseAuthProviderType usedAuthType) : base(message)
        {
            UsedAuthType = usedAuthType;
        }
    }
}