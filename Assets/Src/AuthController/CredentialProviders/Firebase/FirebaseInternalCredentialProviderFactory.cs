using Src.AuthController.CredentialProviders.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public class FirebaseInternalCredentialProviderFactory : IFirebaseInternalCredentialProviderFactory
    {
        public IGoogleCredentialProvider CreateGoogleCredentialProvider()
        {
            return new GoogleCredentialProvider();
        }
    }
}