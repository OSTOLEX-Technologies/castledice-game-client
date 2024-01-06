using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.JwtManagement.Converters.Metamask
{
    public interface IMetamaskJwtConverter
    {
        public JwtStore FromMetamaskAuthResponse(MetamaskAccessTokenResponse response);

        public JwtStore FromMetamaskRefreshResponse(MetamaskRefreshTokenResponse response);
    }
}