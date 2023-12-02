using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto26;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Responses;
using UnityEngine;

namespace Src.AuthController.CredentialProviders
{
    public static class GoogleCredentialProvider
    {
        private const string ApiKey = "AIzaSyDJKKgNOH6a0HRVpkR4rn42eAgqF8Umrso";
        
        private const string ClientId = "150757942999-m2nn366uakk12l8clf3pj7rfuvj6a04v.apps.googleusercontent.com";
        private const string ClientSecret = "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";
        
        private const int LoopbackPort = 3303;
        private static readonly string RedirectUri = $"http://localhost:{LoopbackPort}";

        private static readonly HttpPortListener Listener;
        private static string _authCode;
        
        private static GoogleIdTokenResponse _googleApiResponse;
        private static float _googleApiResponseIssueTime;
        private const float AccessTokenValidityMargin = 30f;


        public static async Task<GoogleIdTokenResponse> GetCredentialAsync()
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

        private static bool ValidateAccessToken()
        {
            int.TryParse(_googleApiResponse.expires_in, out var validityPeriod);

            return (Time.time - _googleApiResponseIssueTime) <
                   (validityPeriod - AccessTokenValidityMargin);
        }
        
        private static void GetAuthData(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            Application.OpenURL($"https://accounts.google.com/o/oauth2/v2/auth?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=email");

            Listener.StartListening(code =>
            {
                _authCode = code;
                
                ExchangeAuthCodeWithIdToken(googleIdTokenResponse =>
                {
                    //SingInWithToken(idToken, "google.com");
                    _googleApiResponse = googleIdTokenResponse;
                    tcs.SetResult(_googleApiResponse);
                });
                
                Listener.StopListening();
            });
        }
        
        private static void ExchangeAuthCodeWithIdToken(Action<GoogleIdTokenResponse> callback)
        {
            RestClient.Request(new RequestHelper
            {
                Method = "POST",
                Uri = "https://oauth2.googleapis.com/token",
                Params = new Dictionary<string, string>
                {
                    {"code", _authCode},
                    {"client_id", ClientId},
                    {"client_secret", ClientSecret},
                    {"redirect_uri", RedirectUri},
                    {"grant_type","authorization_code"}
                }
            
            }).Then(
                response =>
                {
                    var data = JsonUtility.FromJson<GoogleIdTokenResponse>(response.Text);
                    callback(data);
                }).Catch(Debug.Log);
        }
        
        private static void RefreshAccessToken(TaskCompletionSource<GoogleIdTokenResponse> tcs)
        {
            RestClient.Request(new RequestHelper
            {
                Method = "POST",
                Uri = "https://oauth2.googleapis.com/token",
                Params = new Dictionary<string, string>
                {
                    {"code", _authCode},
                    {"client_id", ClientId},
                    {"client_secret", ClientSecret},
                    {"redirect_uri", RedirectUri},
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
        

        
        /*private static void SingInWithToken(string token, string providerId)
        {
            var payLoad =
                $"{{\"postBody\":\"id_token={token}&providerId={providerId}\",\"requestUri\":\"http://localhost\",\"returnIdpCredential\":true,\"returnSecureToken\":true}}";
            RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={ApiKey}", payLoad).Then(
                response =>
                {
                    Debug.Log(response.Text);
                }).Catch(Debug.Log);
        }*/
    }
}