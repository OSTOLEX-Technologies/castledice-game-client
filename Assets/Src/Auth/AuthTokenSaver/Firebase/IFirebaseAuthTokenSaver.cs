using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver.Firebase
{
    public interface IFirebaseAuthTokenSaver : IAuthTokenSaver
    {
        public void TryGetTokenStoreByAuthType(out JwtStore store, FirebaseAuthProviderType providerType);
        public void TryGetGoogleTokenStore(out GoogleJwtStore store);
        public void SaveAuthTokens(JwtStore store, FirebaseAuthProviderType providerType);
        public void SaveGoogleAuthTokens(GoogleJwtStore store);
    }
}