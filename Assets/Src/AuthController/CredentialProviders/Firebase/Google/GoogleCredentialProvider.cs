using System.Threading.Tasks;
using Src.AuthController.AuthKeys;
using Src.AuthController.CredentialProviders.Firebase.Google.UrlOpening;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.REST_Request_Proxies;
using Src.AuthController.REST.REST_Response_DTOs;
using UnityEngine;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public class GoogleCredentialProvider : IGoogleCredentialProvider
    {
        private readonly IGoogleRestRequestsAdapter _googleRestRequestsAdapter;
        private readonly IGoogleOAuthUrl _oAuthUrl;
        private readonly ILocalHttpPortListener _localHttpPortListener;

        private string _authCode;
        
        private GoogleIdTokenResponse _googleApiResponse;
        private float _googleApiResponseIssueTime;
        private const float AccessTokenValidityMargin = 30f;

        public GoogleCredentialProvider(IGoogleRestRequestsAdapter googleRestRequestsAdapter, IGoogleOAuthUrl oAuthUrl, ILocalHttpPortListener localHttpPortListener)
        {
            _googleRestRequestsAdapter = googleRestRequestsAdapter;
            _oAuthUrl = oAuthUrl;
            _localHttpPortListener = localHttpPortListener;
        }
        
        public async Task<GoogleIdTokenResponse> GetCredentialAsync()
        {
            if (_googleApiResponse != null)
            {
                if (ValidateAccessToken()) 
                {
                    return _googleApiResponse;
                }

                var responseTcs = new TaskCompletionSource<GoogleRefreshTokenResponse>();
                
                RefreshAccessToken(responseTcs);
                
                await responseTcs.Task;
                var response = responseTcs.Task.Result;

                _googleApiResponse.access_token = response.access_token;
                _googleApiResponse.expires_in = response.expires_in;
                
                return _googleApiResponse;
            }
            else
            {
                var responseTcs = new TaskCompletionSource<GoogleIdTokenResponse>();
                
                GetAuthData(responseTcs);
                
                await responseTcs.Task;
                _googleApiResponse = responseTcs.Task.Result;
                return _googleApiResponse;
            }
        }

        private bool ValidateAccessToken()
        {
            int.TryParse(_googleApiResponse.expires_in, out var validityPeriod);

            return (Time.time - _googleApiResponseIssueTime) <
                   (validityPeriod - AccessTokenValidityMargin);
        }
        
        private void GetAuthData(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            _oAuthUrl.Open();

            _localHttpPortListener.StartListening(code =>
            {
                _authCode = code;
                
                ExchangeAuthCodeWithIdToken(tcs);
                
                _localHttpPortListener.StopListening();
            });
        }
        
        private void ExchangeAuthCodeWithIdToken(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            var requestParamsDto = new GoogleIdTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _authCode,
                GoogleAuthConfig.Verifier,
                GoogleAuthConfig.RedirectUri);
            
            _googleRestRequestsAdapter.ExchangeAuthCodeWithIdToken(requestParamsDto.AsDictionary(), tcs);
        }
        
        private void RefreshAccessToken(TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            var requestParamsDto = new GoogleRefreshTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _googleApiResponse.refresh_token);
            
            _googleRestRequestsAdapter.RefreshAccessToken(requestParamsDto.AsDictionary(), tcs);
        }
    }
}