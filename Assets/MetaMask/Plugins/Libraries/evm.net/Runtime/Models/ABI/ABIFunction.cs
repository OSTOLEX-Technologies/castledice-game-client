using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models.ABI
{
    public class ABIFunction : ABIDef
    {
        [JsonProperty("outputs")]
        public ABIParameter[] Outputs { get; set; }
        
        [JsonProperty("stateMutability")]
        public ABIStateMutability StateMutability { get; set; }
    }
}