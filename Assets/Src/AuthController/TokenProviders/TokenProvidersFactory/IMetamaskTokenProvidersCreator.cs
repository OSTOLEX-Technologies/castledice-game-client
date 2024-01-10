using System.Threading.Tasks;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public interface IMetamaskTokenProvidersCreator
    {
        public Task<MetamaskTokenProvider> GetTokenProviderAsync();
    }
}