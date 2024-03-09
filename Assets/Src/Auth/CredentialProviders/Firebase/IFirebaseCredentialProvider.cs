using System.Threading.Tasks;
using Firebase.Auth;

namespace Src.Auth.CredentialProviders.Firebase
{
    public interface IFirebaseCredentialProvider
    {
        public Task<Credential> GetCredentialAsync(AuthType authProviderType);
        
        public void InterruptGoogleProviderInit();
    }
}