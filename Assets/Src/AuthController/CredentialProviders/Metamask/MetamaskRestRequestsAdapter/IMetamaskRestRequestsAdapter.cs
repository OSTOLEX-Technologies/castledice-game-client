using System.Threading.Tasks;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter
{
    public interface IMetamaskRestRequestsAdapter
    {
        public void GetNonce(MetamaskNonceRequestDtoProxy requestParams,
            TaskCompletionSource<MetamaskNonceResponse> tcs);
        public void AuthenticateAndGetTokens(MetamaskAuthRequestDtoProxy requestParams, TaskCompletionSource<MetamaskAccessTokenResponse> tcs);
        public void RefreshAccessTokens(MetamaskRefreshRequestDtoProxy requestParams, TaskCompletionSource<MetamaskRefreshTokenResponse> tcs);
    }
}