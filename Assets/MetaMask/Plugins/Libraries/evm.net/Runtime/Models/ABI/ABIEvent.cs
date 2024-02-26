using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models.ABI
{
    public class ABIEvent : ABIDef
    {
        [JsonProperty("anonymous")]
        public bool Anonymous { get; set; }
    }
}