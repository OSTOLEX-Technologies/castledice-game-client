using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Request_Proxies
{
    [Serializable]
    public class GoogleRefreshTokenRequestDtoProxy
    {
        [JsonProperty("client_id")]
        public string clientID;

        [JsonProperty("client_secret")]
        public string clientSecret;

        [JsonProperty("grant_type")]
        public const string GrantType = "refresh_token";

        [JsonProperty("refresh_token")]
        public string refreshToken;

        public GoogleRefreshTokenRequestDtoProxy(string clientID, string clientSecret, string refreshToken)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
            this.refreshToken = refreshToken;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "client_id", clientID },
                { "client_secret", clientSecret },
                { "refresh_token", refreshToken },
                { "grant_type", GrantType }
            };
        }
    }
}