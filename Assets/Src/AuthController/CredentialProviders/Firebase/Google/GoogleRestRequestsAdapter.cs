using System.Collections.Generic;
using System.Threading.Tasks;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public class GoogleRestRequestsAdapter : IGoogleRestRequestsAdapter
    {
        private const string TokenAccessUri = "https://oauth2.googleapis.com/token";
        private readonly IRestClientRequestAdapter _restClientRequestAdapter;

        public GoogleRestRequestsAdapter(IRestClientRequestAdapter restClientRequestAdapter)
        {
            _restClientRequestAdapter = restClientRequestAdapter;
        }
        
        public void ExchangeAuthCodeWithIdToken(Dictionary<string, string> requestParams,TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            _restClientRequestAdapter.Request(RestRequestMethodType.Post, TokenAccessUri, requestParams, tcs);
        }
        
        public void RefreshAccessToken(Dictionary<string, string> requestParams, TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            _restClientRequestAdapter.Request(RestRequestMethodType.Post, TokenAccessUri,
                requestParams, tcs);
        }
    }
}