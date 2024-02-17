using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver.Metamask
{
    public interface IMetamaskAuthTokenSaver : IAuthTokenSaver
    {
        private const string MetamaskStorePrefNamePrefix = "Metamask";
        protected static string MetamaskStorePrefName =>
            MetamaskStorePrefNamePrefix + IAuthTokenSaver.StorePrefNamePostfix;

        public void TryGetMetamaskAuthTokenStore(out JwtStore store);
        public void SaveMetamaskAuthTokens(JwtStore store);
    }
}