using Src.Auth.AuthKeys;
using Src.Auth.AuthKeys;
using MetamaskAuthConfig = Src.Auth.AuthKeys.MetamaskAuthConfig;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider
{
    public class MetamaskBackendUrlProvider : IMetamaskBackendUrlProvider 
    {
        public string GetNonceUrl => $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.NonceGetterEndpoint}";
        public string GetAuthUrl => $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.AuthEndpoint}";
        public string GetRefreshUrl => $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.RefreshEndpoint}";
    }
}