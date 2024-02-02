using Src.Auth.AuthKeys;
using Src.Auth.CredentialProviders.Firebase.Google;
using Src.Auth.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.Auth.Exceptions.Authorization;
using Src.Auth.JwtManagement.Converters.Google;
using Src.Auth.REST;
using Src.Auth.REST.PortListener;
using Src.Auth.REST.PortListener.ListenerContextInterpretation;
using Src.Auth.REST.PortListener.ListenerContextResponse;
using Src.Auth.UrlOpening;

namespace Src.Auth.CredentialProviders.Firebase
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