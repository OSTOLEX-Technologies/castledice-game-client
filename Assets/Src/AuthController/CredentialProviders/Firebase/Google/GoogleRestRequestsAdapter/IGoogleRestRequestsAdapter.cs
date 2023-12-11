using System.Threading.Tasks;
using Src.AuthController.REST.REST_Request_Proxies;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter
{
    public interface IGoogleRestRequestsAdapter
    {
        public void ExchangeAuthCodeWithIdToken(GoogleIdTokenRequestDtoProxy requestParams,
            TaskCompletionSource<GoogleIdTokenResponse> tcs);

        public void RefreshAccessToken(GoogleRefreshTokenRequestDtoProxy requestParams,
            TaskCompletionSource<GoogleRefreshTokenResponse> tcs);
    }
}