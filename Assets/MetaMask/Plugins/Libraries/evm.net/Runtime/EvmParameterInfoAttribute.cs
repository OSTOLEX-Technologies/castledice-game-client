using System;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime
{
    public class EvmParameterInfoAttribute : Attribute
    {
        public string Type { get; set; }

        public string Name { get; set; }
    }
}