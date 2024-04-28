using Src.Auth.REST.REST_Response_DTOs.Firebase.Google;

namespace Src.Auth.JwtManagement.DtoConverters.Google
{
    public interface IGoogleJwtConverter
    {
        public GoogleJwtStore FromGoogleAuthResponse(GoogleIdTokenResponse response);

        public GoogleJwtStore FromGoogleRefreshResponse(GoogleJwtStore oldStore, GoogleRefreshTokenResponse response);
    }
}