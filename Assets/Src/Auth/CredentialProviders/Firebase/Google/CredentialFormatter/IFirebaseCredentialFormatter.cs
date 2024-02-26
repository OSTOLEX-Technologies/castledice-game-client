using Firebase.Auth;
using Src.Auth.JwtManagement;

namespace Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter
{
    public interface IFirebaseCredentialFormatter
    {
        public Credential FormatCredentials(GoogleJwtStore googleCredentials);
    }
}