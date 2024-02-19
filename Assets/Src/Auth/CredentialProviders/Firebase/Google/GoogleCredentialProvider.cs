using System.Threading.Tasks;
using Src.Auth.AuthKeys;
using Src.Auth.AuthTokenSaver.Firebase;
using Src.Auth.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.Auth.JwtManagement;
using Src.Auth.JwtManagement.Converters.Google;
using Src.Auth.REST.PortListener;
using Src.Auth.REST.REST_Request_Proxies.Firebase.Google;
using Src.Auth.REST.REST_Response_DTOs.Firebase.Google;
using Src.Auth.UrlOpening;
using UnityEngine;

namespace Src.Auth.CredentialProviders.Firebase.Google
{
    public class GoogleCredentialProvider : IGoogleCredentialProvider
    {
        private bool TokenIsStored => _tokenStore != null;
        
        private readonly IGoogleRestRequestsAdapter _restRequestsAdapter;
        private readonly IUrlOpener _oAuthUrlOpener;
        private readonly ILocalHttpPortListener _localHttpPortListener;
        private readonly IGoogleJwtConverter _jwtConverter;
        private readonly IFirebaseAuthTokenSaver _authTokenSaver;

        private GoogleJwtStore _tokenStore;

        public GoogleCredentialProvider(
            IGoogleRestRequestsAdapter restRequestsAdapter, 
            IUrlOpener oAuthUrlOpener, 
            ILocalHttpPortListener localHttpPortListener,
            IGoogleJwtConverter jwtConverter,
            IFirebaseAuthTokenSaver authTokenSaver)
        {
            _restRequestsAdapter = restRequestsAdapter;
            _oAuthUrlOpener = oAuthUrlOpener;
            _localHttpPortListener = localHttpPortListener;
            _jwtConverter = jwtConverter;
            _authTokenSaver = authTokenSaver;
            _authTokenSaver.TryGetGoogleTokenStore(out _tokenStore);
        }
        
        public async Task<GoogleJwtStore> GetCredentialAsync()
        {
            if (!TokenIsStored)
            {
                Debug.Log("GOOGLE TOKENS AREN'T STORED");
                var authResponse = await GetAuthData();;
                
                _tokenStore = _jwtConverter.FromGoogleAuthResponse(authResponse);
                _authTokenSaver.SaveGoogleAuthTokens(_tokenStore);

                return _tokenStore;
            }

            if (!_tokenStore.accessToken.Valid)
            {
                Debug.Log("GOOGLE ACCESS TOKEN IS INVALID");
                var refreshResponse = await RefreshAccessToken();

                _tokenStore = _jwtConverter.FromGoogleRefreshResponse(_tokenStore, refreshResponse);
                _authTokenSaver.SaveGoogleAuthTokens(_tokenStore);

                return _tokenStore;
            }

            Debug.Log("GOOGLE TOKEN IS VALID");
            return _tokenStore;
        }

        private async Task<GoogleIdTokenResponse> GetAuthData()
        {
            var idResponseTcs = new TaskCompletionSource<GoogleIdTokenResponse>();
            
            _localHttpPortListener.StartListening(authCode => 
            {
                Debug.Log("<GOOGLE AUTH CODE RECEIVING CALLBACK>, code: " + authCode);
                ExchangeAuthCodeWithIdToken(idResponseTcs, authCode);
            });
            
            _oAuthUrlOpener.Open(GoogleAuthConfig.GoogleOAuthUrl);
            
            var idResponse = await idResponseTcs.Task;
            
            return idResponse;
        }
        
        private void ExchangeAuthCodeWithIdToken(TaskCompletionSource<GoogleIdTokenResponse> idResponseTcs, string authCode)
        {
            var requestParamsDto = new GoogleIdTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                authCode,
                GoogleAuthConfig.Verifier,
                GoogleAuthConfig.RedirectUri);
            
            _restRequestsAdapter.ExchangeAuthCodeWithIdToken(requestParamsDto, idResponseTcs);
        }
        
        private async Task<GoogleRefreshTokenResponse> RefreshAccessToken()
        {
            var requestParamsDto = new GoogleRefreshTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _tokenStore.refreshToken.Token);
            
            var response = await _restRequestsAdapter.RefreshAccessToken(requestParamsDto);

            return response;
        }
    }
}