using System;
using Newtonsoft.Json;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend.Service;

namespace Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend
{
    [Serializable]
    public class MetamaskNonceResponse
    {
        [JsonProperty("id")]
        public int ID { get; private set; }

        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("providers")]
        public MetamaskNonceResponseProvider[] Providers { get; private set; }

        [JsonProperty("public_address")]
        public string PublicAddress { get; private set; }

        
        [JsonProperty("nonce")]
        public string Nonce { get; private set; }
    }
}