using Src.AuthController.CredentialProviders.Firebase.Google;
using Src.AuthController.REST;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public class FirebaseInternalCredentialProviderFactory : IFirebaseInternalCredentialProviderFactory
    {
        public IGoogleCredentialProvider CreateGoogleCredentialProvider()
        {
            return new GoogleCredentialProvider(
                new GoogleRestRequestsAdapter(
                    new RestClientRequestAdapter()));
        }
    }
}