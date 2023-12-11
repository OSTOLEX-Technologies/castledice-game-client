using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs
{
    [Serializable]
    public class GoogleRefreshTokenResponse
    {
        [JsonProperty]
        public string accessToken;

        [JsonProperty]
        //In seconds
        public string expiresIn;

        [JsonProperty]
        public string scope;

        [JsonProperty]
        public string tokenType;
    }
}