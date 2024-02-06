using Src.Auth.CredentialProviders.Firebase.Google;

namespace Src.Auth.CredentialProviders.Firebase
{
    public interface IFirebaseInternalCredentialProviderCreator
    {
        public IGoogleCredentialProvider CreateGoogleCredentialProvider();
    }
}