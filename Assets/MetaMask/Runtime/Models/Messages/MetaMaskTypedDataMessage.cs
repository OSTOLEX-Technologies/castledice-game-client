using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MetaMask.Runtime.Models.Messages
{
    public class MetaMaskTypedDataMessage<T>
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}