using evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models;
using ImpromptuInterface;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Factory
{
    public class ImpromptuContractFactory : IContractFactory
    {
        public T BuildNewInstance<T>(IProvider provider, EvmAddress address) where T : class
        {
            return new Contract(provider, address, typeof(T)).ActLike<T>();
        }
    }
}