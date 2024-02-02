using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models
{
    public class JsonRpcResult<TR> : JsonRpcBase
    {
        [JsonProperty("result")]
        public TR Result { get; protected set; }
    }
}