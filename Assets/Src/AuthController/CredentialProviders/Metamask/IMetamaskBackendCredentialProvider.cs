using System.Threading.Tasks;

namespace Src.AuthController.CredentialProviders.Metamask
{
    public interface IMetamaskBackendCredentialProvider
    {
        public Task<string> GetCredentialAsync();
    }
}