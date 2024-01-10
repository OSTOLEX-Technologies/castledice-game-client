using Firebase.Auth;
using Src.AuthController.JwtManagement;

namespace Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter
{
    public class FirebaseCredentialFormatter : IFirebaseCredentialFormatter
    {
        public Credential FormatCredentials(GoogleJwtStore googleCredentials)
        {
            return GoogleAuthProvider.GetCredential(
                googleCredentials.IdToken.Token, 
                googleCredentials.AccessToken.Token);
        }
    }
}