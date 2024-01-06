using Firebase.Auth;
using Src.AuthController.JwtManagement;

namespace Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter
{
    public interface IFirebaseCredentialFormatter
    {
        public Credential FormatCredentials(GoogleJwtStore googleCredentials);
    }
}