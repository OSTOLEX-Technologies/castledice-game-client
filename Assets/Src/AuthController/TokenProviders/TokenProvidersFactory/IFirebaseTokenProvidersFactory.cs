using System.Threading.Tasks;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public interface IFirebaseTokenProvidersFactory
    {
        public Task<FirebaseTokenProvider> GetTokenProviderAsync(FirebaseAuthProviderType authProviderType);
    }
}