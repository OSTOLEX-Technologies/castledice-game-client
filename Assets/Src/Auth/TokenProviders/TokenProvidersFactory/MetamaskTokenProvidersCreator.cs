using System.Threading.Tasks;
using Src.Auth.AuthTokenSaver.Metamask;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.CredentialProviders.Metamask;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider;
using Src.Auth.JwtManagement.Converters.Metamask;
using Src.Auth.REST;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
{
    public class MetamaskTokenProvidersCreator : IMetamaskTokenProvidersCreator
    {
        private readonly IMetamaskBackendCredentialProvider _backendCredentialProvider;

        public MetamaskTokenProvidersCreator()
        {
            var authTokenSaver = new MetamaskAuthTokenPlayerPrefsSaver(
                new PlayerPrefsStringSaver());
            
            _backendCredentialProvider = new MetamaskBackendCredentialProvider(
                new MetamaskWalletFacade(),
                new MetamaskSignerFacade(),
                new MetamaskRestRequestsAdapter(
                    new HttpClientRequestAdapter(),
                    new MetamaskBackendUrlProvider()),
                new MetamaskJwtConverter(),
                authTokenSaver);
        }

        public async Task<MetamaskTokenProvider> GetTokenProviderAsync()
        {
            return new MetamaskTokenProvider(_backendCredentialProvider);
        }
    }
}