using System;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EvmConstructorMethodAttribute : Attribute
    {
        public string Bytecode { get; set; }
        
        public EvmConstructorMethodAttribute()
        {
        }
    }
}