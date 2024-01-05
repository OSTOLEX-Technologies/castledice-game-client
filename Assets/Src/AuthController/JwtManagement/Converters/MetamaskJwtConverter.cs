using System;
using Src.AuthController.JwtManagement.Tokens;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;
using UnityEngine;

namespace Src.AuthController.JwtManagement.Converters
{
    public static class MetamaskJwtConverter
    {
        public static JwtStore FromMetamaskAuthResponse(MetamaskAccessTokenResponse response)
        {
            Debug.Log(response.EncodedJwt + " " + response.ExpiresIn + " " + response.RefreshToken.IssuedAt );
            var accessToken = new JwtToken(response.EncodedJwt,
                response.ExpiresIn,
                DateTime.Now);
            var refreshToken = new JwtToken(response.RefreshToken.Token,
                response.RefreshToken.ExpiresIn,
                DateTime.Now);
            
            return new JwtStore(
                accessToken,
                refreshToken);
        }

        public static JwtStore FromMetamaskRefreshResponse(MetamaskRefreshTokenResponse response)
        {
            var accessToken = new JwtToken(response.EncodedJwt,
                response.ExpiresIn,
                DateTime.Now);
            var refreshToken = new JwtToken(response.RefreshToken.Token,
                response.RefreshToken.ExpiresIn,
                DateTime.Now);

            return new JwtStore(
                accessToken,
                refreshToken);
        }
    }
}