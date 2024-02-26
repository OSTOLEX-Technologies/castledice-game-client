using System.Text.Json.Serialization;
using evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models;
using Newtonsoft.Json;

namespace MetaMask.Runtime.Models.Messages
{
    public class MetaMaskDataMessage
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public JsonRpcPayload Data { get; set; }
    }
}