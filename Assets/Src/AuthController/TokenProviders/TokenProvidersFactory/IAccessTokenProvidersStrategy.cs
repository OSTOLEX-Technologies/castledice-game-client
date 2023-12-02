using System.Threading.Tasks;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public interface IAccessTokenProvidersStrategy
    {
        Task<IAccessTokenProvider> GetAccessTokenProviderAsync(AuthType authType);
    }
}