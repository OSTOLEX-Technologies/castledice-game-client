using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models.ABI
{
    public class FunctionAbiInput : FunctionAbiOutput
    {
        [JsonProperty("indexed")]
        public bool Indexed { get; set; }
    }
}