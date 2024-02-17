using Newtonsoft.Json;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver.Metamask
{
    public class MetamaskAuthTokenPlayerPrefsSaver : IMetamaskAuthTokenSaver
    {
        private readonly IPlayerPrefsStringSaver _playerPrefsSaver;

        public MetamaskAuthTokenPlayerPrefsSaver(IPlayerPrefsStringSaver playerPrefsSaver)
        {
            _playerPrefsSaver = playerPrefsSaver;
        }

        public void TryGetMetamaskAuthTokenStoreAsync(out JwtStore store)
        {
            if (_playerPrefsSaver.TryGetStringValue(
                    IMetamaskAuthTokenSaver.MetamaskStorePrefName, 
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
            _playerPrefsSaver.SaveStringValue(
                IMetamaskAuthTokenSaver.MetamaskStorePrefName, 
                serializedStore);
        }
    }
}