using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MetaMask.Runtime.Models.Messages
{
    public class TypedMessage
    {
        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}