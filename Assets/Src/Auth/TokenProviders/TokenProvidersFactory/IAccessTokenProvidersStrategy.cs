using System.Threading.Tasks;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
{
    public interface IAccessTokenProvidersStrategy
    {
        Task<IAccessTokenProvider> GetAccessTokenProviderAsync(AuthType authType);
    }
}