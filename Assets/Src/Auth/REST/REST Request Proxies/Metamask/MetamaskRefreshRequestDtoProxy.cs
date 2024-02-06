using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.Auth.REST.REST_Request_Proxies.Metamask
{
    [Serializable]
    public class MetamaskRefreshRequestDtoProxy
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; private set; }

        public MetamaskRefreshRequestDtoProxy(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "refresh_token", RefreshToken },
            };
        }
    }
}