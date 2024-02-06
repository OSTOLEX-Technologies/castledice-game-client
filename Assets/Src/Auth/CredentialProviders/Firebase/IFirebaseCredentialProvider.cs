using System.Threading.Tasks;
using Firebase.Auth;

namespace Src.Auth.CredentialProviders.Firebase
{
    public interface IFirebaseCredentialProvider
    {
        Task<Credential> GetCredentialAsync(FirebaseAuthProviderType authProviderType);
    }
}