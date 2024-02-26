using System.Threading.Tasks;
using Src.Auth.REST.REST_Request_Proxies.Metamask;
using Src.Auth.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter
{
    public interface IMetamaskRestRequestsAdapter
    {
        public Task<MetamaskNonceResponse> GetNonce(MetamaskNonceRequestDtoProxy requestParams);
        public Task<MetamaskAccessTokenResponse> AuthenticateAndGetTokens(MetamaskAuthRequestDtoProxy requestParams);
        public Task<MetamaskRefreshTokenResponse> RefreshAccessTokens(MetamaskRefreshRequestDtoProxy requestParams);
    }
}