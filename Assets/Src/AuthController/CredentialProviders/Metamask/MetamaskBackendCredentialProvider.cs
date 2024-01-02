using System;
using System.Threading.Tasks;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.CredentialProviders.Metamask
{
    public class MetamaskBackendCredentialProvider : IMetamaskBackendCredentialProvider
    {
        public async Task<MetamaskAccessTokenResponse> GetCredentialAsync()
        {
            throw new NotImplementedException();
        }
    }
}