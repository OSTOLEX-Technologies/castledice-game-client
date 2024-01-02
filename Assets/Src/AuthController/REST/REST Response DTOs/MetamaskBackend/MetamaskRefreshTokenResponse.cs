using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend
{
    [Serializable]
    public class MetamaskRefreshTokenResponse
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; private set; }
        
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; private set; }
        
        [JsonProperty("issued_at")]
        public string IssuedAt { get; private set; }
    }
}