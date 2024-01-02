using System.Threading.Tasks;
using Src.AuthController.CredentialProviders.Metamask;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public class MetamaskTokenProvidersFactory : IMetamaskTokenProvidersFactory
    {
        private readonly IMetamaskBackendCredentialProvider _backendCredentialProvider;

        public MetamaskTokenProvidersFactory(IMetamaskBackendCredentialProvider backendCredentialProvider)
        {
            _backendCredentialProvider = backendCredentialProvider;
        }

        public async Task<MetamaskTokenProvider> GetTokenProviderAsync()
        {
            return new MetamaskTokenProvider(_backendCredentialProvider);
        }
    }
}