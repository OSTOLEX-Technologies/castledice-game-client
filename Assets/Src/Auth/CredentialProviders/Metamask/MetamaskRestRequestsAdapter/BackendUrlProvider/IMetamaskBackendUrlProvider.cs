namespace Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider
{
    public interface IMetamaskBackendUrlProvider
    {
        public string GetNonceUrl { get; }
        public string GetAuthUrl { get; }
        public string GetRefreshUrl { get; }
    }
}