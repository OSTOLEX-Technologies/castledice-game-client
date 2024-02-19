using Newtonsoft.Json;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver.Firebase
{
    public class FirebaseAuthTokenSaver : IFirebaseAuthTokenSaver
    {
        private readonly IPlayerPrefsStringSaver _playerPrefsSaver;

        public FirebaseAuthTokenSaver(IPlayerPrefsStringSaver playerPrefsSaver)
        {
            _playerPrefsSaver = playerPrefsSaver;
        }
        
        public void TryGetTokenStoreByAuthType(out JwtStore store, FirebaseAuthProviderType providerType)
        {
            if (_playerPrefsSaver.TryGetStringValue(
                    FormatFirebaseStorePrefNameByAuthType(providerType), 
                    out var storedValue))
            {
                store = JsonConvert.DeserializeObject<JwtStore>(storedValue);
                return;
            }

            store = null;
        }
        public void TryGetGoogleTokenStore(out GoogleJwtStore store)
        {
            if (_playerPrefsSaver.TryGetStringValue(
                    FormatFirebaseStorePrefNameByAuthType(FirebaseAuthProviderType.Google), 
                    out var storedValue))
            {
                store = JsonConvert.DeserializeObject<GoogleJwtStore>(storedValue);
                return;
            }

            store = null;
        }

        public void SaveAuthTokens(JwtStore store, FirebaseAuthProviderType providerType)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _playerPrefsSaver.SaveStringValue(
                FormatFirebaseStorePrefNameByAuthType(providerType), 
                serializedStore);
        }
        
        public void SaveGoogleAuthTokens(GoogleJwtStore store)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _playerPrefsSaver.SaveStringValue(
                FormatFirebaseStorePrefNameByAuthType(FirebaseAuthProviderType.Google), 
                serializedStore);
        }

        private string FormatFirebaseStorePrefNameByAuthType(FirebaseAuthProviderType providerType)
        {
            return providerType + IAuthTokenSaver.StorePrefNamePostfix;
        }
    }
}