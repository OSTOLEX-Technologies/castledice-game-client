using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend.Service
{
    [Serializable]
    public class MetamaskNonceResponseProvider
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("unique_id")]
        public string UniqueId { get; private set; }
    }
}