using Firebase.Auth;
using Src.Auth.JwtManagement;

namespace Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter
{
    public class FirebaseCredentialFormatter : IFirebaseCredentialFormatter
    {
        public Credential FormatCredentials(GoogleJwtStore googleCredentials)
        {
            return GoogleAuthProvider.GetCredential(
                googleCredentials.idToken.Token, 
                googleCredentials.accessToken.Token);
        }
    }
}