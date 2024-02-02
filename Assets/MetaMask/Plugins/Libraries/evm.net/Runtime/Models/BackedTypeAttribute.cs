using System;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class BackedTypeAttribute : Attribute
    {
        public Type ImplementationType { get; }

        public BackedTypeAttribute(Type implementationType)
        {
            this.ImplementationType = implementationType;
        }
    }
}