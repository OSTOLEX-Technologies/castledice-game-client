using System.Net.Http;
using System.Threading.Tasks;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider;
using Src.Auth.REST;
using Src.Auth.REST.REST_Request_Proxies.Metamask;
using Src.Auth.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter
{
    public class MetamaskRestRequestsAdapter : IMetamaskRestRequestsAdapter
    {
        private readonly IHttpClientRequestAdapter _httpClientRequestAdapter;
        private readonly IMetamaskBackendUrlProvider _urlProvider;

        public MetamaskRestRequestsAdapter(IHttpClientRequestAdapter httpClientRequestAdapter, IMetamaskBackendUrlProvider urlProvider)
        {
            _httpClientRequestAdapter = httpClientRequestAdapter;
            _urlProvider = urlProvider;
        }

        public async Task<MetamaskNonceResponse> GetNonce(MetamaskNonceRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<MetamaskNonceResponse>();
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                _urlProvider.GetNonceUrl,
                requestParams.AsDictionary(), 
                tcs);
            return await tcs.Task;
        }

        public async Task<MetamaskAccessTokenResponse> AuthenticateAndGetTokens(MetamaskAuthRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<MetamaskAccessTokenResponse>();
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                _urlProvider.GetAuthUrl,
                requestParams.AsDictionary(),
                tcs);
            return await tcs.Task;
        }

        public async Task<MetamaskRefreshTokenResponse> RefreshAccessTokens(MetamaskRefreshRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<MetamaskRefreshTokenResponse>();
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                _urlProvider.GetRefreshUrl,
                requestParams.AsDictionary(),
                tcs);
            return await tcs.Task;
        }
    }
}