using System.Threading.Tasks;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public interface IMetamaskTokenProvidersFactory
    {
        public Task<MetamaskTokenProvider> GetTokenProviderAsync();
    }
}