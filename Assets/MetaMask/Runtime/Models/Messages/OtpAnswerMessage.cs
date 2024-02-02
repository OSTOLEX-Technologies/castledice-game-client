using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MetaMask.Runtime.Models.Messages
{
    public class OtpAnswerMessage : TypedMessage
    {
        [JsonProperty("otpAnswer")]
        [JsonPropertyName("otpAnswer")]
        public int OtpAnswer { get; set; }
    }
}