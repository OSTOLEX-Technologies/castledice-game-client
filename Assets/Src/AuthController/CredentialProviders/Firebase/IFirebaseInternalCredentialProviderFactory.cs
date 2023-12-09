using Src.AuthController.CredentialProviders.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public interface IFirebaseInternalCredentialProviderFactory
    {
        public IGoogleCredentialProvider CreateGoogleCredentialProvider();
    }
}