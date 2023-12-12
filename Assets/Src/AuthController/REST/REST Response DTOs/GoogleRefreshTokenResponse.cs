using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs
{
    [Serializable]
    public class GoogleRefreshTokenResponse
    {
        [JsonProperty("access_token")]
        public string accessToken;

        [JsonProperty("expires_in")]
        //In seconds
        public float expiresIn;

        [JsonProperty("scope")]
        public string scope;

        [JsonProperty("token_type")]
        public string tokenType;
    }
}