using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Request_Proxies
{
    [Serializable]
    public class GoogleRefreshTokenRequestDtoProxy
    {
        [JsonProperty("client_id")]
        public string ClientID { get; private set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; private set; }

        [JsonProperty("grant_type")]
        public const string GrantType = "refresh_token";

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; private set; }

        public GoogleRefreshTokenRequestDtoProxy(string clientID, string clientSecret, string refreshToken)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
            RefreshToken = refreshToken;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "refresh_token", RefreshToken },
                { "grant_type", GrantType }
            };
        }
    }
}