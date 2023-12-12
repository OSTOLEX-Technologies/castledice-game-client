using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs
{
    [Serializable]
    public class GoogleIdTokenResponse
    {
        [JsonProperty("access_token")]
        public string accessToken;
        
        [JsonProperty("expires_in")]
        //In seconds
        public float expiresIn;
        
        [JsonProperty("id_token")]
        public string idToken;

        [JsonProperty("refresh_token")]
        public string refreshToken;

        [JsonProperty("scope")]
        public string scope;

        [JsonProperty("token_type")]
        public string tokenType;
    }
}