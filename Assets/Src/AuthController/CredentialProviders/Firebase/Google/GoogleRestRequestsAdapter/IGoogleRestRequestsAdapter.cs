using System.Threading.Tasks;
using Src.AuthController.REST.REST_Request_Proxies.Firebase.Google;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter
{
    public interface IGoogleRestRequestsAdapter
    {
        public void ExchangeAuthCodeWithIdToken(GoogleIdTokenRequestDtoProxy requestParams, TaskCompletionSource<GoogleIdTokenResponse> tcs);
        public Task<GoogleRefreshTokenResponse> RefreshAccessToken(GoogleRefreshTokenRequestDtoProxy requestParams);
    }
}