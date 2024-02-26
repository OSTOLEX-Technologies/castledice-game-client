using System.Threading.Tasks;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
{
    public interface IMetamaskTokenProvidersCreator
    {
        public Task<MetamaskTokenProvider> GetTokenProviderAsync();
    }
}