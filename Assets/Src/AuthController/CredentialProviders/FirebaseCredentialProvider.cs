using System.Threading.Tasks;
using Firebase.Auth;
using Src.AuthController.CredentialProviders;
using Src.AuthController.CredentialProviders.Google;
using Src.AuthController.Exceptions;

namespace Src.AuthController
{
    public class FirebaseCredentialProvider : IFirebaseCredentialProvider
    {
        public FirebaseCredentialProvider()
        {
        }
        
        public async Task<Credential> GetCredentialAsync(FirebaseAuthProviderType authProviderType)
        {
            switch (authProviderType)
            {
                case FirebaseAuthProviderType.Google:
                    var googleCredentialProvider = new GoogleCredentialProvider();
                    var googleCredentials = await googleCredentialProvider.GetCredentialAsync();
                    Credential credential = GoogleAuthProvider.GetCredential(googleCredentials.id_token, googleCredentials.access_token);
                    return credential;
                default:
                    throw new FirebaseCredentialProviderNotFoundException(authProviderType);
            }
        }
    }
}