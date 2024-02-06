using System.Threading.Tasks;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Signer
{
    public interface IMetamaskSignerFacade
    {
        public Task<string> Sign(string message);
    }
}