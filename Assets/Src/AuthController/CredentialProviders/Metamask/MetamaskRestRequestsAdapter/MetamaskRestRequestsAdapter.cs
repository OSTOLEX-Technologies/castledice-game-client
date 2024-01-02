using System.Collections.Generic;
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

        public void GetNonce(TaskCompletionSource<MetamaskNonceResponse> tcs)
        {
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.NonceGetterUrl}", 
                new Dictionary<string, string>(), 
                tcs);
        }

        public void AuthenticateAndGetTokens(MetamaskAuthRequestDtoProxy requestParams, TaskCompletionSource<MetamaskAccessTokenResponse> tcs)
        {
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.AuthUrl}", 
                requestParams.AsDictionary(),
                tcs);
        }

        public void RefreshAccessTokens(MetamaskRefreshRequestDtoProxy requestParams, TaskCompletionSource<MetamaskRefreshTokenResponse> tcs)
        {
            _httpClientRequestAdapter.Request(
                HttpMethod.Get, 
                $"{MetamaskAuthConfig.GlobalUrl}{MetamaskAuthConfig.RefreshUrl}", 
                requestParams.AsDictionary(),
                tcs);
        }
    }
}