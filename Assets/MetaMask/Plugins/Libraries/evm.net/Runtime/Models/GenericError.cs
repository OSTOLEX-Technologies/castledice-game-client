using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models
{
    public class GenericError
    {
        [JsonProperty("code")]
        public long Code { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}