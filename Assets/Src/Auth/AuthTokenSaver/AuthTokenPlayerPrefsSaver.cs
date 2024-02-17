using Newtonsoft.Json;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public class AuthTokenPlayerPrefsSaver : IAuthTokenSaver
    {
        private const string MetamaskStorePrefNamePrefix = "Metamask";
        private string MetamaskStorePrefName => MetamaskStorePrefNamePrefix + IAuthTokenSaver.StorePrefNamePostfix;

        private readonly IPlayerPrefsStringSaver _playerPrefsSaver;

        public AuthTokenPlayerPrefsSaver(IPlayerPrefsStringSaver playerPrefsSaver)
        {
            _playerPrefsSaver = playerPrefsSaver;
        }

        public void TryGetMetamaskAuthTokenStoreAsync(out JwtStore store)
        {
            if (_playerPrefsSaver.TryGetStringValue(
                    MetamaskStorePrefName, 
                    out var storedValue))
            {
                store = JsonConvert.DeserializeObject<JwtStore>(storedValue);
                return;
            }

            store = null;
        }

        public void TryGetFirebaseTokenStoreByAuthTypeAsync(out JwtStore store, FirebaseAuthProviderType providerType)
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

        public void SaveMetamaskAuthTokensAsync(JwtStore store)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _playerPrefsSaver.SaveStringValue(MetamaskStorePrefName, serializedStore);
        }

        public void SaveFirebaseAuthTokensAsync(JwtStore store, FirebaseAuthProviderType providerType)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _playerPrefsSaver.SaveStringValue(FormatFirebaseStorePrefNameByAuthType(providerType), serializedStore);
        }

        private string FormatFirebaseStorePrefNameByAuthType(FirebaseAuthProviderType providerType)
        {
            return providerType + IAuthTokenSaver.StorePrefNamePostfix;
        }
    }
}