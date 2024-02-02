using System.Threading.Tasks;
using Src.Auth.CredentialProviders.Metamask;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
{
    public class MetamaskTokenProvidersCreator : IMetamaskTokenProvidersCreator
    {
        private readonly IMetamaskBackendCredentialProvider _backendCredentialProvider;

        public MetamaskTokenProvidersCreator(IMetamaskBackendCredentialProvider backendCredentialProvider)
        {
            _backendCredentialProvider = backendCredentialProvider;
        }

        public async Task<MetamaskTokenProvider> GetTokenProviderAsync()
        {
            return new MetamaskTokenProvider(_backendCredentialProvider);
        }
    }
}