using System.Threading.Tasks;
using Src.AuthController.AuthKeys;
using Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.AuthController.DeepLinking.Config;
using Src.AuthController.DeepLinking.LinkResolver.LinkFormatter;
using Src.AuthController.JwtManagement;
using Src.AuthController.JwtManagement.Converters.Google;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.REST_Request_Proxies.Firebase.Google;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;
using Src.AuthController.UrlOpening;
using UnityEngine;

namespace Src.AuthController.CredentialProviders.Firebase.Google
{
    public class GoogleCredentialProvider : IGoogleCredentialProvider
    {
        private bool TokenIsStored => _tokenStore != null;
        
        private readonly IGoogleRestRequestsAdapter _restRequestsAdapter;
        private readonly IUrlOpener _oAuthUrlOpener;
        private readonly ILocalHttpPortListener _localHttpPortListener;
        private readonly IDeepLinkFormatter _linkFormatter;
        private readonly IGoogleJwtConverter _jwtConverter;

        private GoogleJwtStore _tokenStore;

        public GoogleCredentialProvider(
            IGoogleRestRequestsAdapter restRequestsAdapter, 
            IUrlOpener oAuthUrlOpener, 
            ILocalHttpPortListener localHttpPortListener,
            IDeepLinkFormatter linkFormatter,
            IGoogleJwtConverter jwtConverter)
        {
            _restRequestsAdapter = restRequestsAdapter;
            _oAuthUrlOpener = oAuthUrlOpener;
            _localHttpPortListener = localHttpPortListener;
            _linkFormatter = linkFormatter;
            _jwtConverter = jwtConverter;
        }
        
        public async Task<GoogleJwtStore> GetCredentialAsync()
        {
            if (!TokenIsStored)
            {
                var authResponse = await GetAuthData();;

                _tokenStore = _jwtConverter.FromGoogleAuthResponse(authResponse);

                return _tokenStore;
            }

            if (!_tokenStore.AccessToken.Valid)
            {
                var refreshResponse = await RefreshAccessToken();

                _tokenStore = _jwtConverter.FromGoogleRefreshResponse(_tokenStore, refreshResponse);

                return _tokenStore;
            }

            return _tokenStore;
        }

        private async Task<GoogleIdTokenResponse> GetAuthData()
        {
            var idResponseTcs = new TaskCompletionSource<GoogleIdTokenResponse>();

            _oAuthUrlOpener.Open(GoogleAuthConfig.GoogleOAuthUrl);
            
            _localHttpPortListener.StartListening(authCode =>
            {
                ExchangeAuthCodeWithIdToken(idResponseTcs, authCode);
                
                _localHttpPortListener.StopListening();
            });

            var idResponse = await idResponseTcs.Task;

            return idResponse;
        }
        
        private void ExchangeAuthCodeWithIdToken(TaskCompletionSource<GoogleIdTokenResponse> idResponseTcs, string authCode)
        {
            Debug.Log(_linkFormatter.FormatLink(DeepLinkConfig.GoogleAuthRedirectUri));
            
            var requestParamsDto = new GoogleIdTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                authCode,
                GoogleAuthConfig.Verifier,
                _linkFormatter.FormatLink(DeepLinkConfig.GoogleAuthRedirectUri));
            
            _restRequestsAdapter.ExchangeAuthCodeWithIdToken(requestParamsDto, idResponseTcs);
        }
        
        private async Task<GoogleRefreshTokenResponse> RefreshAccessToken()
        {
            var requestParamsDto = new GoogleRefreshTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _tokenStore.RefreshToken.Token);
            
            var response = await _restRequestsAdapter.RefreshAccessToken(requestParamsDto);

            return response;
        }
    }
}