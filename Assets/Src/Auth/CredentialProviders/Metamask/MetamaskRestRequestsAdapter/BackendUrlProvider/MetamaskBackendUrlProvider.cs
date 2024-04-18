using Src.Auth.AuthKeys;
using Src.Auth.AuthKeys;
using MetamaskAuthConfig = Src.Auth.AuthKeys.MetamaskAuthConfig;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider
{
    public class MetamaskBackendUrlProvider : IMetamaskBackendUrlProvider 
    {
        public string GetNonceUrl => $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.NonceGetterUrl}";
        public string GetAuthUrl => $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.AuthUrl}";
        public string GetRefreshUrl => $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.RefreshUrl}";
    }
}