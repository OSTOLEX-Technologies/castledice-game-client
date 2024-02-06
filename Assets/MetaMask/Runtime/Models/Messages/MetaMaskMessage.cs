using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MetaMask.Runtime.Models.Messages
{

    /// <summary>
    /// Represents a message that is sent to the wallet.
    /// </summary>
    public class MetaMaskMessage
    {

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("message")]
        [JsonPropertyName("message")]
        public object Message { get; set; }

    }
}
