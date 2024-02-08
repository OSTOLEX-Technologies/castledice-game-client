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
            return await _httpClientRequestAdapter.Request<MetamaskNonceResponse>(
                HttpMethod.Get, 
                _urlProvider.GetNonceUrl,
                requestParams.AsDictionary());
        }

        public async Task<MetamaskAccessTokenResponse> AuthenticateAndGetTokens(MetamaskAuthRequestDtoProxy requestParams)
        {
            return await _httpClientRequestAdapter.Request<MetamaskAccessTokenResponse>(
                HttpMethod.Get, 
                _urlProvider.GetAuthUrl,
                requestParams.AsDictionary());
        }

        public async Task<MetamaskRefreshTokenResponse> RefreshAccessTokens(MetamaskRefreshRequestDtoProxy requestParams)
        {
            return await _httpClientRequestAdapter.Request<MetamaskRefreshTokenResponse>(
                HttpMethod.Get, 
                _urlProvider.GetRefreshUrl,
                requestParams.AsDictionary());
        }
    }
}