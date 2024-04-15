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
        
        public bool TryGetTokenStoreByAuthType(out AbstractJwtStore store, AuthType providerType)
        {
            if (_saver.TryGetStringValue(
                    GetStorePrefNameByAuthType(providerType), 
                    out var storedValue))
            {
                var converter = new JwtStoreJsonConverter();
                store = JsonConvert.DeserializeObject<AbstractJwtStore>(
                    storedValue, converter);
                return true;
            }
            
            store = NullJwtStore.Instance;
            return false;
        }

        #endregion


        #region Saving Store

        public void SaveAuthTokens(AbstractJwtStore store, AuthType providerType)
        {
            var serializedStore = JsonConvert.SerializeObject(store); 
            _saver.SaveStringValue(
                GetStorePrefNameByAuthType(providerType), 
                serializedStore);
            UpdateLastAuthType(providerType);
        }


        #endregion


        #region Last Login Info

        public bool TryGetLastAuthType(out AuthType authType)
        {
            authType = AuthType.Google;
            if (!_saver.TryGetStringValue(
                    LastLoginStoreInfoPrefName,
                    out var storedValue)) return false;
            
            Enum.TryParse(storedValue, out authType);
            return true;

        }

        private void UpdateLastAuthType(AuthType authType)
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