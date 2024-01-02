using System.Threading.Tasks;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.CredentialProviders.Metamask
{
    public interface IMetamaskBackendCredentialProvider
    {
        public Task<MetamaskAccessTokenResponse> GetCredentialAsync();
    }
}