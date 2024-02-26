using System;
using System.Net.Http;
using System.Threading.Tasks;
using Src.Auth.Exceptions.Authorization;
using Src.Auth.REST;
using Src.Auth.REST.REST_Request_Proxies.Firebase.Google;
using Src.Auth.REST.REST_Response_DTOs.Firebase.Google;

namespace Src.Auth.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter
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
        
        public async void ExchangeAuthCodeWithIdToken(GoogleIdTokenRequestDtoProxy requestParams, TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            try
            {
                var res = await _httpClientRequestAdapter.Request<GoogleIdTokenResponse>(HttpMethod.Post, TokenAccessUri,
                    requestParams.AsDictionary());
                tcs.SetResult(res);
            }
            catch (Exception e)
            {
                throw _authExceptionCreator.FormatAuthException(e);
            }
        }
        
        public async Task<GoogleRefreshTokenResponse> RefreshAccessToken(GoogleRefreshTokenRequestDtoProxy requestParams)
        {
            try
            {
                return await _httpClientRequestAdapter.Request<GoogleRefreshTokenResponse>(HttpMethod.Post, TokenAccessUri,
                    requestParams.AsDictionary());
            }
            catch (Exception e)
            {
                throw _authExceptionCreator.FormatAuthException(e);
            }
        }
    }
}