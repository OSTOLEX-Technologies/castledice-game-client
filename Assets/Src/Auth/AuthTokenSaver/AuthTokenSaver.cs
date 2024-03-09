using System;
using Newtonsoft.Json;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.JwtManagement;
using Src.Auth.JwtManagement.JsonConverters;

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
        
        public void TryGetTokenStoreByAuthType(out AbstractJwtStore store, AuthType providerType)
        {
            if (_saver.TryGetStringValue(
                    GetStorePrefNameByAuthType(providerType), 
                    out var storedValue))
            {
                var converter = new JwtStoreDataConverter();
                store = JsonConvert.DeserializeObject<AbstractJwtStore>(
                    storedValue, converter);
                return;
            }

            store = null;
        }

        #endregion


        #region Saving Store

        public void SaveAuthTokens(AbstractJwtStore store, AuthType providerType)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _saver.SaveStringValue(
                GetStorePrefNameByAuthType(providerType), 
                serializedStore);
            UpdateLastLoginInfo(providerType);
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