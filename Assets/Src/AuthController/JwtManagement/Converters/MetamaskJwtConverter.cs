using System;
using Src.AuthController.JwtManagement.Tokens;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.JwtManagement.Converters
{
    public static class MetamaskJwtConverter
    {
        public static JwtStore FromMetamaskAuthResponse(MetamaskAccessTokenResponse response)
        {
            var accessToken = new JwtToken(response.EncodedJwt,
                response.ExpiresIn,
                Convert.ToDateTime(TimeSpan.FromSeconds(response.RefreshToken.IssuedAt)));
            var refreshToken = new JwtToken(response.RefreshToken.Token,
                response.RefreshToken.ExpiresIn,
                Convert.ToDateTime(TimeSpan.FromSeconds(response.RefreshToken.IssuedAt)));
            
            return new JwtStore(
                accessToken,
                refreshToken);
        }

        public static JwtStore FromMetamaskRefreshResponse(MetamaskRefreshTokenResponse response)
        {
            var accessToken = new JwtToken(response.EncodedJwt,
                response.ExpiresIn,
                Convert.ToDateTime(TimeSpan.FromSeconds(response.RefreshToken.IssuedAt)));
            var refreshToken = new JwtToken(response.RefreshToken.Token,
                response.RefreshToken.ExpiresIn,
                Convert.ToDateTime(TimeSpan.FromSeconds(response.RefreshToken.IssuedAt)));

            return new JwtStore(
                accessToken,
                refreshToken);
        }
    }
}