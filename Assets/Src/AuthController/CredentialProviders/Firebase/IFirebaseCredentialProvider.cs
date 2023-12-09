using System.Threading.Tasks;
using Firebase.Auth;

namespace Src.AuthController.CredentialProviders.Firebase
{
    public interface IFirebaseCredentialProvider
    {
        Task<Credential> GetCredentialAsync(FirebaseAuthProviderType authProviderType);
    }
}