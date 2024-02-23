using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public interface IAuthTokenSaver
    {
        protected const string StorePrefNamePostfix = "AuthTokensStore";
        protected const string LastLoginStoreInfoPrefName = "LastLoginAuuthTokensStoreInfo";
        
        public void TryGetTokenStoreByAuthType(out JwtStore store, AuthType providerType);
        public void TryGetGoogleTokenStore(out GoogleJwtStore store);
        public void SaveAuthTokens(JwtStore store, AuthType providerType);
        public void SaveGoogleAuthTokens(GoogleJwtStore store);

        public bool TryGetLastLoginInfo(out AuthType authType);
        public void UpdateLastLoginInfo(AuthType authType);
    }
}