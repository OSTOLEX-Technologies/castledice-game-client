using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Request_Proxies
{
    [Serializable]
    public class GoogleIdTokenRequestDtoProxy
    {
        [JsonProperty]
        public string clientID;

        [JsonProperty]
        public string clientSecret;

        [JsonProperty]
        public string code;

        [JsonProperty]
        public string codeVerifier;

        [JsonProperty]
        public const string GrantType = "authorization_code";

        [JsonProperty]
        public string redirectUri;

        public GoogleIdTokenRequestDtoProxy(string clientID, string clientSecret, string code, string codeVerifier, string redirectUri)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
            this.code = code;
            this.codeVerifier = codeVerifier;
            this.redirectUri = redirectUri;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientID },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", GrantType }
            };
        }
    }
}