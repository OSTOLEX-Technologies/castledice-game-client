using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto26;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Responses;
using Src.AuthController.AuthKeys;
using UnityEngine;

namespace Src.AuthController.CredentialProviders
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
            TaskCompletionSource<GoogleIdTokenResponse> responseTcs =
                new TaskCompletionSource<GoogleIdTokenResponse>();
            
            if (_googleApiResponse != null)
            {
                if (ValidateAccessToken()) 
                {
                    return _googleApiResponse;
                }

                RefreshAccessToken(responseTcs);
            }
            else
            {
                GetAuthData(responseTcs);
            }

            await responseTcs.Task;
            return _googleApiResponse;
        }

        private bool ValidateAccessToken()
        {
            int.TryParse(_googleApiResponse.expires_in, out var validityPeriod);

            return (Time.time - _googleApiResponseIssueTime) <
                   (validityPeriod - AccessTokenValidityMargin);
        }
        
        private void GetAuthData(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            Application.OpenURL($"https://accounts.google.com/o/oauth2/v2/auth?client_id={GoogleAuthConfig.ClientId}&redirect_uri={_redirectUri}&response_type=code&scope=email");

            var listener = new HttpPortListener(LoopbackPort);
            listener.StartListening(code =>
            {
                _authCode = code;
                
                ExchangeAuthCodeWithIdToken(googleIdTokenResponse =>
                {
                    _googleApiResponse = googleIdTokenResponse;
                    tcs.SetResult(_googleApiResponse);
                });
                
                listener.StopListening();
            });
        }
        
        private void ExchangeAuthCodeWithIdToken(Action<GoogleIdTokenResponse> callback)
        {
            RestClient.Request(new RequestHelper
            {
                Method = "POST",
                Uri = "https://oauth2.googleapis.com/token",
                Params = new Dictionary<string, string>
                {
                    {"code", _authCode},
                    {"client_id", GoogleAuthConfig.ClientId},
                    {"client_secret", GoogleAuthConfig.ClientSecret},
                    {"redirect_uri", _redirectUri},
                    {"grant_type","authorization_code"}
                }
            
            }).Then(
                response =>
                {
                    var data = JsonUtility.FromJson<GoogleIdTokenResponse>(response.Text);
                    callback(data);
                }).Catch(Debug.Log);
        }
        
        private void RefreshAccessToken(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            RestClient.Request(new RequestHelper
            {
                Method = "POST",
                Uri = "https://oauth2.googleapis.com/token",
                Params = new Dictionary<string, string>
                {
                    {"code", _authCode},
                    {"client_id", GoogleAuthConfig.ClientId},
                    {"client_secret", GoogleAuthConfig.ClientSecret},
                    {"redirect_uri", _redirectUri},
                    {"grant_type","authorization_code"}
                }
            
            }).Then(
                response =>
                {
                    var data = JsonUtility.FromJson<GoogleRefreshTokenResponse>(response.Text);
                    _googleApiResponse.expires_in = data.expires_in;
                    _googleApiResponse.access_token = data.access_token;
                    
                    tcs.SetResult(_googleApiResponse);
                }).Catch(Debug.Log);
        }
    }
}