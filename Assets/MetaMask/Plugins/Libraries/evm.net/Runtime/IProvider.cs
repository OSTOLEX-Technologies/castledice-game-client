using System.Threading.Tasks;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime
{
    public interface IProvider : ILegacyProvider
    {
        Task<TR> Request<TR>(string method, object[] parameters = null);
    }
}