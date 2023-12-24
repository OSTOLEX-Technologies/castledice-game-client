using System.Net.Http;
using System.Threading.Tasks;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Request_Proxies;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter
{
    public class GoogleRestRequestsAdapter : IGoogleRestRequestsAdapter
    {
        private const string TokenAccessUri = "https://oauth2.googleapis.com/token";
        private readonly IHttpClientRequestAdapter _httpClientRequestAdapter;

        public GoogleRestRequestsAdapter(IHttpClientRequestAdapter httpClientRequestAdapter)
        {
            _httpClientRequestAdapter = httpClientRequestAdapter;
        }
        
        public void ExchangeAuthCodeWithIdToken(GoogleIdTokenRequestDtoProxy requestParams,TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            _httpClientRequestAdapter.Request(HttpMethod.Post, TokenAccessUri, 
                requestParams.AsDictionary(), tcs);
        }
        
        public void RefreshAccessToken(GoogleRefreshTokenRequestDtoProxy requestParams, TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            _httpClientRequestAdapter.Request(HttpMethod.Post, TokenAccessUri,
                requestParams.AsDictionary(), tcs);
        }
    }
}