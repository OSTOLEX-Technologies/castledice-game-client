using evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Factory
{
    public interface IContractFactory
    {
        T BuildNewInstance<T>(IProvider provider, EvmAddress address = null) where T : class;
    }
}