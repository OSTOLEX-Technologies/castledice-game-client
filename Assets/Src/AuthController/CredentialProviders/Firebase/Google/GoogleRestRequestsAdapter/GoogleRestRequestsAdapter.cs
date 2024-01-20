using System;
using System.Net.Http;
using System.Threading.Tasks;
using Src.AuthController.Exceptions.Authorization;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Request_Proxies.Firebase.Google;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter
{
    public class GoogleRestRequestsAdapter : IGoogleRestRequestsAdapter
    {
        private const string TokenAccessUri = "https://oauth2.googleapis.com/token";
        private readonly IHttpClientRequestAdapter _httpClientRequestAdapter;
        private readonly IGoogleAuthExceptionCreator _authExceptionCreator;

        public GoogleRestRequestsAdapter(IHttpClientRequestAdapter httpClientRequestAdapter, IGoogleAuthExceptionCreator authExceptionCreator)
        {
            _httpClientRequestAdapter = httpClientRequestAdapter;
            _authExceptionCreator = authExceptionCreator;
        }
        
        public void ExchangeAuthCodeWithIdToken(GoogleIdTokenRequestDtoProxy requestParams, TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            try
            {
                _httpClientRequestAdapter.Request(HttpMethod.Post, TokenAccessUri,
                    requestParams.AsDictionary(), tcs);
            }
            catch (Exception e)
            {
                throw _authExceptionCreator.FormatAuthException(e);
            }
        }
        
        public async Task<GoogleRefreshTokenResponse> RefreshAccessToken(GoogleRefreshTokenRequestDtoProxy requestParams)
        {
            var tcs = new TaskCompletionSource<GoogleRefreshTokenResponse>();
            try
            {
                _httpClientRequestAdapter.Request(HttpMethod.Post, TokenAccessUri,
                    requestParams.AsDictionary(), tcs);
            }
            catch (Exception e)
            {
                throw _authExceptionCreator.FormatAuthException(e);
            }

            return await tcs.Task;
        }
    }
}