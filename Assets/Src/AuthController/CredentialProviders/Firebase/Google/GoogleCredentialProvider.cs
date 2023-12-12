using System.Threading.Tasks;
using Src.AuthController.AuthKeys;
using Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.AuthController.CredentialProviders.Firebase.Google.TokenValidator;
using Src.AuthController.CredentialProviders.Firebase.Google.UrlOpening;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.REST_Request_Proxies;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public class GoogleCredentialProvider : IGoogleCredentialProvider
    {
        private readonly IGoogleAccessTokenValidator _accessTokenValidator;
        private readonly IGoogleRestRequestsAdapter _restRequestsAdapter;
        private readonly IGoogleOAuthUrl _oAuthUrl;
        private readonly ILocalHttpPortListener _localHttpPortListener;

        private string _authCode;
        
        private GoogleIdTokenResponse _googleApiResponse;
        private float _googleApiResponseIssueTime;

        public GoogleCredentialProvider(IGoogleAccessTokenValidator accessTokenValidator, IGoogleRestRequestsAdapter restRequestsAdapter, IGoogleOAuthUrl oAuthUrl, ILocalHttpPortListener localHttpPortListener)
        {
            _accessTokenValidator = accessTokenValidator;
            _restRequestsAdapter = restRequestsAdapter;
            _oAuthUrl = oAuthUrl;
            _localHttpPortListener = localHttpPortListener;
        }
        
        public async Task<GoogleIdTokenResponse> GetCredentialAsync()
        {
            if (_googleApiResponse != null)
            {
                if (_accessTokenValidator.ValidateAccessToken(_googleApiResponse, _googleApiResponseIssueTime))
                {
                    return _googleApiResponse;
                }

                var responseTcs = new TaskCompletionSource<GoogleRefreshTokenResponse>();
                
                RefreshAccessToken(responseTcs);
                
                await responseTcs.Task;
                var response = responseTcs.Task.Result;

                _googleApiResponse.accessToken = response.accessToken;
                _googleApiResponse.expiresIn = response.expiresIn;
                
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
            
            _restRequestsAdapter.ExchangeAuthCodeWithIdToken(requestParamsDto, tcs);
        }
        
        private void RefreshAccessToken(TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            var requestParamsDto = new GoogleRefreshTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _googleApiResponse.refreshToken);
            
            _restRequestsAdapter.RefreshAccessToken(requestParamsDto, tcs);
        }
    }
}