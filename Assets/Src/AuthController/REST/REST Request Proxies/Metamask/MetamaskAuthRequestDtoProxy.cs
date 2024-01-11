using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Request_Proxies.Metamask
{
    [Serializable]
    public class MetamaskAuthRequestDtoProxy
    {
        [JsonProperty("public_address")]
        public string PublicAddress { get; private set; }
        
        [JsonProperty("signed_message")]
        public string SignedMessage { get; private set; }

        public MetamaskAuthRequestDtoProxy(string publicAddress, string signedMessage)
        {
            PublicAddress = publicAddress;
            SignedMessage = signedMessage;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "public_address", PublicAddress },
                { "signed_message", SignedMessage },
            };
        }
    }
}