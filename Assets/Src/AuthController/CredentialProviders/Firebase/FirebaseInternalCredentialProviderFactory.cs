using Src.AuthController.AuthKeys;
using Src.AuthController.CredentialProviders.Firebase.Google;
using Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.AuthController.CredentialProviders.Firebase.Google.TokenValidator;
using Src.AuthController.CredentialProviders.Firebase.Google.UrlOpening;
using Src.AuthController.REST;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.PortListener.ListenerContextInterpretation;
using Src.AuthController.REST.PortListener.ListenerContextResponse;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public class FirebaseInternalCredentialProviderFactory : IFirebaseInternalCredentialProviderFactory
    {
        public IGoogleCredentialProvider CreateGoogleCredentialProvider()
        {
            return new GoogleCredentialProvider(
                new GoogleAccessTokenValidator(),
                new GoogleRestRequestsAdapter(
                    new HttpClientRequestAdapter()),
                new GoogleOAuthUrl(),
                new LocalHttpPortListener(
                    new HttpPortListenerHandler(
                        GoogleAuthConfig.LoopbackPort,
                        new HttpListenerContextInterpreter(),
                        GoogleAuthConfig.AuthCodeQueryKeyName,
                        new HttpListenerContextResponse(),
                        GoogleAuthConfig.ResponseHtml)));
        }
    }
}