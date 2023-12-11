using System.Threading.Tasks;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Request_Proxies;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter
{
    public class GoogleRestRequestsAdapter : IGoogleRestRequestsAdapter
    {
        private const string TokenAccessUri = "https://oauth2.googleapis.com/token";
        private readonly IRestClientRequestAdapter _restClientRequestAdapter;

        public GoogleRestRequestsAdapter(IRestClientRequestAdapter restClientRequestAdapter)
        {
            _restClientRequestAdapter = restClientRequestAdapter;
        }
        
        public void ExchangeAuthCodeWithIdToken(GoogleIdTokenRequestDtoProxy requestParams,TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            _restClientRequestAdapter.Request(RestRequestMethodType.Post, TokenAccessUri, 
                requestParams.AsDictionary(), tcs);
        }
        
        public void RefreshAccessToken(GoogleRefreshTokenRequestDtoProxy requestParams, TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            _restClientRequestAdapter.Request(RestRequestMethodType.Post, TokenAccessUri,
                requestParams.AsDictionary(), tcs);
        }
    }
}