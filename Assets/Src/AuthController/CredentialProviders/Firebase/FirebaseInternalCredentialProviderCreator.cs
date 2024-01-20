using Src.AuthController.AuthKeys;
using Src.AuthController.CredentialProviders.Firebase.Google;
using Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.AuthController.Exceptions.Authorization;
using Src.AuthController.JwtManagement.Converters.Google;
using Src.AuthController.REST;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.PortListener.ListenerContextInterpretation;
using Src.AuthController.REST.PortListener.ListenerContextResponse;
using Src.AuthController.UrlOpening;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public class FirebaseInternalCredentialProviderCreator : IFirebaseInternalCredentialProviderCreator
    {
        public IGoogleCredentialProvider CreateGoogleCredentialProvider()
        {
            return new GoogleCredentialProvider(
                new GoogleRestRequestsAdapter(
                    new HttpClientRequestAdapter(),
                    new GoogleAuthExceptionCreator()),
                new UrlOpener(),
                new LocalHttpPortListener(
                    new HttpPortListenerHandler(
                        GoogleAuthConfig.LoopbackPort,
                        new HttpListenerContextInterpreter(),
                        GoogleAuthConfig.AuthCodeQueryKeyName,
                        new HttpListenerContextResponse(),
                        GoogleAuthConfig.ResponseHtml)),
                new GoogleJwtConverter());
        }
    }
}