using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public interface IAuthTokenSaver
    {
        public bool TryGetTokenStoreByAuthType(out AbstractJwtStore store, AuthType providerType);
        public void SaveAuthTokens(AbstractJwtStore store, AuthType providerType);

        public bool TryGetLastAuthType(out AuthType authType);
    }
}