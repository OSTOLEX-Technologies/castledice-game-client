using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public interface IAuthTokenSaver
    {
        public void TryGetTokenStoreByAuthType(out AbstractJwtStore store, AuthType providerType);
        public void SaveAuthTokens(AbstractJwtStore store, AuthType providerType);
        public void DeleteAuthTokens();

        public bool TryGetLastLoginInfo(out AuthType authType);
        public void UpdateLastLoginInfo(AuthType authType);
    }
}