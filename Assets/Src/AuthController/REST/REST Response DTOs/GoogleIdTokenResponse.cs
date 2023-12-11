using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs
{
    [Serializable]
    public class GoogleIdTokenResponse
    {
        [JsonProperty]
        public string accessToken;


        [JsonProperty]
        //In seconds
        public string expiresIn;
        
        [JsonProperty]
        public string idToken;

        [JsonProperty]
        public string refreshToken;

        [JsonProperty]
        public string scope;

        [JsonProperty]
        public string tokenType;
    }
}