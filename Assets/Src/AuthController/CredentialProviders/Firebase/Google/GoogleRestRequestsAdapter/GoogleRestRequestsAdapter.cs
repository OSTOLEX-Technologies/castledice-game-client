using System.Net.Http;
using System.Threading.Tasks;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Request_Proxies.Firebase.Google;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;

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
        
        public void ExchangeAuthCodeWithIdToken(GoogleIdTokenRequestDtoProxy requestParams, TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            _httpClientRequestAdapter.Request(HttpMethod.Post, TokenAccessUri, 
                requestParams.AsDictionary(), tcs);
        }
        
        public async Task<GoogleRefreshTokenResponse> RefreshAccessToken(GoogleRefreshTokenRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<GoogleRefreshTokenResponse>();
            _httpClientRequestAdapter.Request(HttpMethod.Post, TokenAccessUri,
                requestParams.AsDictionary(), tcs);
            return await tcs.Task;
        }
    }
}