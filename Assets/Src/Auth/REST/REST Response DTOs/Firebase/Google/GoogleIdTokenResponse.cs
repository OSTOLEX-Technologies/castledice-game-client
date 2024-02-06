using System;
using Newtonsoft.Json;

namespace Src.Auth.REST.REST_Response_DTOs.Firebase.Google
{
    [Serializable]
    public class GoogleIdTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("expires_in")]
        public float ExpiresInSeconds { get; set; }
        
        [JsonProperty("id_token")]
        public string IDToken { get; private set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; private set; }

        [JsonProperty("scope")]
        public string Scope { get; private set; }

        [JsonProperty("token_type")]
        public string TokenType { get; private set; }
    }
}