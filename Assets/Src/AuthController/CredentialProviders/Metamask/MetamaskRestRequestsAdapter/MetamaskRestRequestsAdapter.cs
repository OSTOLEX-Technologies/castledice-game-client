using System.Net.Http;
using System.Threading.Tasks;
using Src.AuthController.AuthKeys;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter
{
    public class MetamaskRestRequestsAdapter : IMetamaskRestRequestsAdapter
    {
        private readonly IHttpClientRequestAdapter _httpClientRequestAdapter;

        public MetamaskRestRequestsAdapter(IHttpClientRequestAdapter httpClientRequestAdapter)
        {
            _httpClientRequestAdapter = httpClientRequestAdapter;
        }

        public async Task<MetamaskNonceResponse> GetNonce(MetamaskNonceRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<MetamaskNonceResponse>();
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.NonceGetterUrl}", 
                requestParams.AsDictionary(), 
                tcs);
            return await tcs.Task;
        }

        public async Task<MetamaskAccessTokenResponse> AuthenticateAndGetTokens(MetamaskAuthRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<MetamaskAccessTokenResponse>();
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.AuthUrl}", 
                requestParams.AsDictionary(),
                tcs);
            return await tcs.Task;
        }

        public async Task<MetamaskRefreshTokenResponse> RefreshAccessTokens(MetamaskRefreshRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<MetamaskRefreshTokenResponse>();
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.RefreshUrl}", 
                requestParams.AsDictionary(),
                tcs);
            return await tcs.Task;
        }
    }
}