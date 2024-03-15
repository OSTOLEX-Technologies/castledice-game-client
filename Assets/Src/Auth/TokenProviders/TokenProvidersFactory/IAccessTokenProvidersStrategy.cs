using System.Threading.Tasks;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
{
    public interface IAccessTokenProvidersStrategy
    {
        public Task<IAccessTokenProvider> GetAccessTokenProviderAsync(
            AuthType authType);
    }
}