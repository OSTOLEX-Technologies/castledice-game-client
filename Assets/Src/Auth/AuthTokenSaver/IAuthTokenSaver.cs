using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public interface IAuthTokenSaver
    {
        protected const string StorePrefNamePostfix = "AuthTokensStore";
        
        public void TryGetMetamaskAuthTokenStoreAsync(out JwtStore store); 
        public void TryGetFirebaseTokenStoreByAuthTypeAsync(out JwtStore store, FirebaseAuthProviderType providerType);
            
        public void SaveMetamaskAuthTokensAsync(JwtStore store); 
        public void SaveFirebaseAuthTokensAsync(JwtStore store, FirebaseAuthProviderType providerType);
    }
}