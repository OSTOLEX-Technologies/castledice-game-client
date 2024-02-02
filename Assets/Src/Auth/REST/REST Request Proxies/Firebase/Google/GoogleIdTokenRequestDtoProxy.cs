using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.Auth.REST.REST_Request_Proxies.Firebase.Google
{
    [Serializable]
    public class GoogleIdTokenRequestDtoProxy
    {
        [JsonProperty("client_id")]
        public string ClientID { get; private set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; private set; }

        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonProperty("code_verifier")]
        public string CodeVerifier { get; private set; }

        [JsonProperty("grant_type")]
        public const string GrantType = "authorization_code"; 

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; private set; }

        public GoogleIdTokenRequestDtoProxy(string clientID, string clientSecret, string code, string codeVerifier, string redirectUri)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
            Code = code;
            CodeVerifier = codeVerifier;
            RedirectUri = redirectUri;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "code", Code },
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri },
                { "grant_type", GrantType }
            };
        }
    }
}