using System.Threading.Tasks;
using Firebase.Auth;
using Src.AuthController.Exceptions;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public class FirebaseCredentialProvider : IFirebaseCredentialProvider
    {
        private readonly IFirebaseInternalCredentialProviderFactory _internalCredentialProviderFactory;

        public FirebaseCredentialProvider(IFirebaseInternalCredentialProviderFactory internalCredentialProviderFactory)
        {
            _internalCredentialProviderFactory = internalCredentialProviderFactory;
        }
        
        public async Task<Credential> GetCredentialAsync(FirebaseAuthProviderType authProviderType)
        {
            switch (authProviderType)
            {
                case FirebaseAuthProviderType.Google:
                    var googleCredentialProvider = _internalCredentialProviderFactory.CreateGoogleCredentialProvider();
                    var googleCredentials = await googleCredentialProvider.GetCredentialAsync();
                    Credential credential = GoogleAuthProvider.GetCredential(googleCredentials.id_token, googleCredentials.access_token);
                    return credential;
                default:
                    throw new FirebaseCredentialProviderNotFoundException(authProviderType);
            }
        }
    }
}