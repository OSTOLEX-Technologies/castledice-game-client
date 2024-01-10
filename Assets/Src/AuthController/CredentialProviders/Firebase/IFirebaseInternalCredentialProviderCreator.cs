using Src.AuthController.CredentialProviders.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public interface IFirebaseInternalCredentialProviderCreator
    {
        public IGoogleCredentialProvider CreateGoogleCredentialProvider();
    }
}