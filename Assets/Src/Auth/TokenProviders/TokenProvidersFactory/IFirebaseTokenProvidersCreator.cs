using System.Threading.Tasks;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
{
    public interface IFirebaseTokenProvidersCreator
    {
        public Task<FirebaseTokenProvider> GetTokenProviderAsync(AuthType authProviderType);
    }
}