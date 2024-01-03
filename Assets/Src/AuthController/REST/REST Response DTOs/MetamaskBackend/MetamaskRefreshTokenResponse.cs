using System;
using Newtonsoft.Json;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend.Service;

namespace Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend
{
    [Serializable]
    public class MetamaskRefreshTokenResponse
    {
        [JsonProperty("exp")]
        public int ExpiresIn { get; private set; }
        
        [JsonProperty("uid")]
        public int UniqueId { get; private set; }
        
        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("email")]
        public string Email { get; private set; }
        
        [JsonProperty("address")]
        public string Address { get; private set; }
        
        [JsonProperty("encoded_jwt")]
        public string EncodedJwt { get; private set; }
        
        [JsonProperty("refresh_token")]
        public MetamaskBackendRefreshToken RefreshToken { get; private set; }
    }
}