using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Src.AuthController.AuthKeys;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Request_Proxies;
using Src.AuthController.REST.REST_Response_DTOs;
using UnityEngine;

namespace Src.AuthController.CredentialProviders.Google
{
    public class GoogleCredentialProvider
    {
        private const int LoopbackPort = 3303;
        private readonly string _redirectUri = $"http://localhost:{LoopbackPort}";
        
        private string _authCode;
        
        private GoogleIdTokenResponse _googleApiResponse;
        private float _googleApiResponseIssueTime;
        private const float AccessTokenValidityMargin = 30f;
        
        public virtual async Task<GoogleIdTokenResponse> GetCredentialAsync()
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
            Application.OpenURL($"https://accounts.google.com/o/oauth2/v2/auth?client_id=" +
                                $"{GoogleAuthConfig.ClientId}&redirect_uri=" +
                                $"{_redirectUri}&response_type=code&scope=email");

            var listener = new HttpPortListener(LoopbackPort);
            listener.StartListening(code =>
            {
                _authCode = code;
                
                ExchangeAuthCodeWithIdToken(tcs);
                
                listener.StopListening();
            });
        }
        
        private void ExchangeAuthCodeWithIdToken(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            var requestParamsDto = new GoogleIdTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _authCode,
                GoogleAuthConfig.Verifier,
                _redirectUri);
            
            GoogleRestRequestsAdapter.ExchangeAuthCodeWithIdToken(requestParamsDto.AsDictionary(), tcs);
        }
        
        private void RefreshAccessToken(TaskCompletionSource<GoogleRefreshTokenResponse> tcs)
        {
            var requestParamsDto = new GoogleRefreshTokenRequestDtoProxy(
                GoogleAuthConfig.ClientId,
                GoogleAuthConfig.ClientSecret,
                _googleApiResponse.refresh_token);
            
            GoogleRestRequestsAdapter.RefreshAccessToken(requestParamsDto.AsDictionary(), tcs);
        }
    }
}