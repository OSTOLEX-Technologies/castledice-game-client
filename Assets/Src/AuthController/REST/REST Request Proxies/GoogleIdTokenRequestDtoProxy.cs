using System;
using System.Collections.Generic;

namespace Src.AuthController.REST.REST_Request_Proxies
{
    [Serializable]
    public class GoogleIdTokenRequestDtoProxy
    {
        public string client_id;
        
        public string client_secret;

        public string code;

        public string code_verifier;

        public const string grant_type = "authorization_code";

        public string redirect_uri;

        public GoogleIdTokenRequestDtoProxy(string clientID, string clientSecret, string code, string codeVerifier, string redirectUri)
        {
            client_id = clientID;
            client_secret = clientSecret;
            this.code = code;
            code_verifier = codeVerifier;
            redirect_uri = redirectUri;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", client_id },
                { "client_secret", client_secret },
                { "redirect_uri", redirect_uri },
                { "grant_type", grant_type }
            };
        }
    }
}