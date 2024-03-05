using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public interface IAuthTokenSaver
    {
        public void TryGetTokenStoreByAuthType(out JwtStore store, AuthType providerType);
        public void TryGetGoogleTokenStore(out GoogleJwtStore store);
        public void SaveAuthTokens(JwtStore store, AuthType providerType);
        public void SaveGoogleAuthTokens(GoogleJwtStore store);

        public bool TryGetLastLoginInfo(out AuthType authType);
        public void UpdateLastLoginInfo(AuthType authType);
    }
}