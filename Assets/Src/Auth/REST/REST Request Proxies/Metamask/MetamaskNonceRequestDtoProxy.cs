using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.Auth.REST.REST_Request_Proxies.Metamask
{
    [Serializable]
    public class MetamaskNonceRequestDtoProxy
    {
        [JsonProperty("public_address")]
        public string PublicAddress { get; private set; }

        public MetamaskNonceRequestDtoProxy(string publicAddress)
        {
            PublicAddress = publicAddress;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "public_address", PublicAddress },
            };
        }
    }
}