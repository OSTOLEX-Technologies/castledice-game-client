using System;
using Src.Auth.JwtManagement.Tokens;
using Src.Auth.REST.REST_Response_DTOs.Firebase.Google;
using UnityEngine;

namespace Src.Auth.JwtManagement.Converters.Google
{
    public sealed class GoogleJwtConverter : IGoogleJwtConverter
    {
        public GoogleJwtStore FromGoogleAuthResponse(GoogleIdTokenResponse response)
        {
            var idToken = new JwtToken(response.IDToken,
                Int32.MaxValue,
                DateTime.Now);
            var accessToken = new JwtToken(response.AccessToken,
                Mathf.FloorToInt(response.ExpiresInSeconds),
                DateTime.Now);
            var refreshToken = new JwtToken(response.RefreshToken,
                Int32.MaxValue,
                DateTime.Now);
            
            return new GoogleJwtStore(
                idToken,
                accessToken,
                refreshToken);
        }

        public GoogleJwtStore FromGoogleRefreshResponse(GoogleJwtStore oldStore, GoogleRefreshTokenResponse response)
        {
            var accessToken = new JwtToken(response.AccessToken,
                Mathf.FloorToInt(response.ExpiresInSeconds),
                DateTime.Now);

            return new GoogleJwtStore(
                oldStore.IdToken,
                accessToken,
                oldStore.RefreshToken);
        }
    }
}