using System;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EvmMethodInfoAttribute : Attribute
    {
        public string Name { get; set; }

        public bool View { get; set; } = false;

        public string[] Returns { get; set; } = null;

        public EvmMethodInfoAttribute()
        {
        }
    }
}