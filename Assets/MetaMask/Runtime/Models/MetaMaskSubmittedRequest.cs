using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MetaMask.Runtime.Models
{
    public class MetaMaskSubmittedRequest
    {

        [JsonProperty("method")]
        [JsonPropertyName("method")]
        public string Method { get; set; }

    }
}
