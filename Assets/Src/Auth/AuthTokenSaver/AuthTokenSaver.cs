using System;
using Newtonsoft.Json;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public class AuthTokenSaver : IAuthTokenSaver
    {
        private readonly IPlayerPrefsStringSaver _playerPrefsSaver;

        public AuthTokenSaver(IPlayerPrefsStringSaver playerPrefsSaver)
        {
            _playerPrefsSaver = playerPrefsSaver;
        }
        
        #region Getting Store
        
        public void TryGetTokenStoreByAuthType(out JwtStore store, AuthType providerType)
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
                    FormatFirebaseStorePrefNameByAuthType(AuthType.Google), 
                    out var storedValue))
            {
                store = JsonConvert.DeserializeObject<GoogleJwtStore>(storedValue);
                return;
            }

            store = null;
        }
        
        #endregion


        #region Saving Store

        public void SaveAuthTokens(JwtStore store, AuthType providerType)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _playerPrefsSaver.SaveStringValue(
                FormatFirebaseStorePrefNameByAuthType(providerType), 
                serializedStore);
            UpdateLastLoginInfo(providerType);
        }

        public void SaveGoogleAuthTokens(GoogleJwtStore store)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _playerPrefsSaver.SaveStringValue(
                FormatFirebaseStorePrefNameByAuthType(AuthType.Google), 
                serializedStore);
            UpdateLastLoginInfo(AuthType.Google);
        }

        #endregion


        #region Last Login Info

        public bool TryGetLastLoginInfo(out AuthType authType)
        {
            authType = AuthType.Google;
            if (!_playerPrefsSaver.TryGetStringValue(
                    IAuthTokenSaver.LastLoginStoreInfoPrefName,
                    out var storedValue)) return false;
            
            Enum.TryParse(storedValue, out authType);
            return true;

        }

        public void UpdateLastLoginInfo(AuthType authType)
        {
            _playerPrefsSaver.SaveStringValue(
                IAuthTokenSaver.LastLoginStoreInfoPrefName, 
                authType.ToString());
        }

        #endregion
        
        
        private string FormatFirebaseStorePrefNameByAuthType(AuthType providerType)
        {
            return providerType + IAuthTokenSaver.StorePrefNamePostfix;
        }
    }
}