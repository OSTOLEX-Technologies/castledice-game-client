using Firebase.Auth;
using Src.AuthController.REST.REST_Response_DTOs;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter
{
    public class FirebaseCredentialFormatter : IFirebaseCredentialFormatter
    {
        public Credential FormatCredentials(GoogleIdTokenResponse googleCredentials)
        {
            return GoogleAuthProvider.GetCredential(googleCredentials.IDToken, googleCredentials.AccessToken);
        }
    }
}