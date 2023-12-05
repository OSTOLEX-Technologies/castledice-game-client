using System;
using System.Collections.Generic;

namespace Src.AuthController.REST.REST_Request_Proxies
{
    [Serializable]
    public class GoogleRefreshTokenRequestDtoProxy
    {
        public string client_id;
        
        public string client_secret;

        public const string grant_type = "refresh_token";

        public string refresh_token;

        public GoogleRefreshTokenRequestDtoProxy(string clientID, string clientSecret, string refreshToken)
        {
            client_id = clientID;
            client_secret = clientSecret;
            refresh_token = refreshToken;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "client_id", client_id },
                { "client_secret", client_secret },
                { "refresh_token", refresh_token },
                { "grant_type", grant_type }
            };
        }
    }
}