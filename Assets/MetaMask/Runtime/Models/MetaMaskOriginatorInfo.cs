using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MetaMask.Runtime.Models
{
    public class MetaMaskOriginatorInfo
    {

        [JsonProperty("title")] [JsonPropertyName("title")]
        public string Title;

        [JsonProperty("url")] [JsonPropertyName("url")]
        public string Url;

        [JsonProperty("source")]
        [JsonPropertyName("source")]
        public string Source = "";

        [JsonProperty("icon")] [JsonPropertyName("icon")]
        public string Icon;
    }
}
