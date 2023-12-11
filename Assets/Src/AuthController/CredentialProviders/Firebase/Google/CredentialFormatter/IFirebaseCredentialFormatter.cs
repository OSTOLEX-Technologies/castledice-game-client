using Firebase.Auth;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter
{
    public interface IFirebaseCredentialFormatter
    {
        public Credential FormatCredentials(GoogleIdTokenResponse googleCredentials);
    }
}