using System.Threading.Tasks;
using Firebase.Auth;

namespace Src.AuthController
{
    public interface IFirebaseCredentialProvider
    {
        Task<Credential> GetCredentialAsync(FirebaseAuthProviderType authProviderType);
    }
}