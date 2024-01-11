using System.Threading.Tasks;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter
{
    public interface IMetamaskRestRequestsAdapter
    {
        public Task<MetamaskNonceResponse> GetNonce(MetamaskNonceRequestDtoProxy requestParams);
        public Task<MetamaskAccessTokenResponse> AuthenticateAndGetTokens(MetamaskAuthRequestDtoProxy requestParams);
        public Task<MetamaskRefreshTokenResponse> RefreshAccessTokens(MetamaskRefreshRequestDtoProxy requestParams);
    }
}