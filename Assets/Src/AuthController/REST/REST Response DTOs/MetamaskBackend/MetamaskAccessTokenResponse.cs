using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend
{
    [Serializable]
    public class MetamaskAccessTokenResponse
    {
        public MetamaskAccessTokenResponse(string accessToken)
        {
            AccessToken = accessToken;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; private set; }
        
        //TODO: add other one properties
    }
}