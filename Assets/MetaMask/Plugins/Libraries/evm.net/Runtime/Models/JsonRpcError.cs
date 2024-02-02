using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models
{
    public class JsonRpcError : JsonRpcBase
    {
        [JsonProperty("error")]
        public GenericError Error { get; protected set; }
    }
}