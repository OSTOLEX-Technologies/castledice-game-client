using System;
using Newtonsoft.Json;

namespace Src.Auth.REST.REST_Response_DTOs.MetamaskBackend.Service
{
    [Serializable]
    public class MetamaskBackendRefreshToken
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("expires")]
        public int ExpiresIn { get; private set; }
        
        [JsonProperty("issued_at")]
        public int IssuedAt { get; protected set; }
    }
}