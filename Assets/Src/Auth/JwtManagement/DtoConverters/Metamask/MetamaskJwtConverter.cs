using System;
using Src.Auth.JwtManagement.Tokens;
using Src.Auth.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.Auth.JwtManagement.Converters.Metamask
{
    public sealed class MetamaskJwtConverter : IMetamaskJwtConverter
    {
        public JwtStore FromMetamaskAuthResponse(MetamaskAccessTokenResponse response)
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

        public JwtStore FromMetamaskRefreshResponse(MetamaskRefreshTokenResponse response)
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