using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend
{
    [Serializable]
    public class MetamaskNonceResponse
    {
        [JsonProperty("nonce")]
        public string Nonce { get; private set; }
    }
}