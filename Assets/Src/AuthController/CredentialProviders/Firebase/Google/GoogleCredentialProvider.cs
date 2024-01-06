using System.Threading.Tasks;
using Src.AuthController.AuthKeys;
using Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.AuthController.JwtManagement;
using Src.AuthController.JwtManagement.Converters.Google;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.REST_Request_Proxies.Firebase.Google;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;
using Src.AuthController.UrlOpening;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public class GoogleCredentialProvider : IGoogleCredentialProvider
    {
        private readonly IGoogleRestRequestsAdapter _restRequestsAdapter;
        private readonly IUrlOpener _oAuthUrlOpener;
        private readonly ILocalHttpPortListener _localHttpPortListener;
        private readonly IGoogleJwtConverter _jwtConverter;
//
        private GoogleJwtStore _tokenStore;

        public GoogleCredentialProvider(
            IGoogleRestRequestsAdapter restRequestsAdapter, 
            IUrlOpener oAuthUrlOpener, 
            ILocalHttpPortListener localHttpPortListener,
            IGoogleJwtConverter jwtConverter)
        {
            _restRequestsAdapter = restRequestsAdapter;
            _oAuthUrlOpener = oAuthUrlOpener;
            _localHttpPortListener = localHttpPortListener;
            _jwtConverter = jwtConverter;
        }
        
        public async Task<GoogleJwtStore> GetCredentialAsync()
        {
            if (_tokenStore == null)
            {
                var responseTcs = new TaskCompletionSource<GoogleIdTokenResponse>();
                
                GetAuthData(responseTcs);
                
                var authResponse = await responseTcs.Task;

                _tokenStore = _jwtConverter.FromGoogleAuthResponse(authResponse);

                return _tokenStore;
            }

            if (!_tokenStore.AccessToken.Valid)
            {
                var responseTcs = new TaskCompletionSource<GoogleRefreshTokenResponse>();
                
                RefreshAccessToken(responseTcs);
                
                var refreshResponse = await responseTcs.Task;
                
                _tokenStore = _jwtConverter.FromGoogleRefreshResponse(_tokenStore, refreshResponse);

                return _tokenStore;
            }

            return _tokenStore;
        }

        private void GetAuthData(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            _oAuthUrlOpener.Open(GoogleAuthConfig.GoogleOAuthUrl);
            _localHttpPortListener.StartListening(authCode =>
            {
                ExchangeAuthCodeWithIdToken(tcs, authCode);
                
                _localHttpPortListener.StopListening();
            });
        }
        
        private void ExchangeAuthCodeWithIdToken(TaskCompletionSource<GoogleIdTokenResponse> tcs, string authCode)
        {
            var requestParamsDto = new GoogleIdTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                authCode,
                GoogleAuthConfig.Verifier,
                GoogleAuthConfig.RedirectUri);
            
            _restRequestsAdapter.ExchangeAuthCodeWithIdToken(requestParamsDto, tcs);
        }
        
        private void RefreshAccessToken(TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            var requestParamsDto = new GoogleRefreshTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _tokenStore.RefreshToken.GetToken());
            
            _restRequestsAdapter.RefreshAccessToken(requestParamsDto, tcs);
        }
    }
}