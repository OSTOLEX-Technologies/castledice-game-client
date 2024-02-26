using System.Threading.Tasks;

namespace Src.Auth.CredentialProviders.Metamask
{
    public interface IMetamaskBackendCredentialProvider
    {
        public Task<string> GetCredentialAsync();
    }
}