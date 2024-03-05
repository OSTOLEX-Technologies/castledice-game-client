using System;
using Newtonsoft.Json;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.JwtManagement;

namespace Src.Auth.AuthTokenSaver
{
    public class AuthTokenSaver : IAuthTokenSaver
    {
        private const string StorePrefNamePostfix = "AuthTokensStore";
        private const string LastLoginStoreInfoPrefName = "LastLoginAuthTokensStoreInfo";
        
        private readonly IStringSaver _saver;

        public AuthTokenSaver(IStringSaver saver)
        {
            _saver = saver;
        }
        
        #region Getting Store
        
        public void TryGetTokenStoreByAuthType(out JwtStore store, AuthType providerType)
        {
            if (_saver.TryGetStringValue(
                    GetStorePrefNameByAuthType(providerType), 
                    out var storedValue))
            {
                store = JsonConvert.DeserializeObject<JwtStore>(storedValue);
                return;
            }

            store = null;
        }

        public void TryGetGoogleTokenStore(out GoogleJwtStore store)
        {
            if (_saver.TryGetStringValue(
                    GetStorePrefNameByAuthType(AuthType.Google), 
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
            _saver.SaveStringValue(
                GetStorePrefNameByAuthType(providerType), 
                serializedStore);
            UpdateLastLoginInfo(providerType);
        }

        public void SaveGoogleAuthTokens(GoogleJwtStore store)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _saver.SaveStringValue(
                GetStorePrefNameByAuthType(AuthType.Google), 
                serializedStore);
            UpdateLastLoginInfo(AuthType.Google);
        }

        #endregion


        #region Last Login Info

        public bool TryGetLastLoginInfo(out AuthType authType)
        {
            authType = AuthType.Google;
            if (!_saver.TryGetStringValue(
                    LastLoginStoreInfoPrefName,
                    out var storedValue)) return false;
            
            Enum.TryParse(storedValue, out authType);
            return true;

        }

        public void UpdateLastLoginInfo(AuthType authType)
        {
            _saver.SaveStringValue(
                LastLoginStoreInfoPrefName, 
                authType.ToString());
        }

        #endregion
        
        
        public static string GetStorePrefNameByAuthType(AuthType providerType)
        {
            return providerType + StorePrefNamePostfix;
        }
    }
}