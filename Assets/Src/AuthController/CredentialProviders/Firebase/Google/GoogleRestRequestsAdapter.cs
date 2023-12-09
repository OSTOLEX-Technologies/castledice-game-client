using System.Collections.Generic;
using System.Threading.Tasks;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public static class GoogleRestRequestsAdapter
    {
        private const string TokenAccessUri = "https://oauth2.googleapis.com/token";
        
        public static void ExchangeAuthCodeWithIdToken(Dictionary<string, string> requestParams,TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            RestClientRequestAdapter.Request(RestRequestMethodType.Post, TokenAccessUri, requestParams, tcs);
        }
        
        public static void RefreshAccessToken(Dictionary<string, string> requestParams, TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            RestClientRequestAdapter.Request(RestRequestMethodType.Post, TokenAccessUri,
                requestParams, tcs);
        }
    }
}