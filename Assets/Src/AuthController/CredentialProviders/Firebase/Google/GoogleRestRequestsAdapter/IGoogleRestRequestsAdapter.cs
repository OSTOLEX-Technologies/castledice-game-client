using System.Collections.Generic;
using System.Threading.Tasks;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter
{
    public interface IGoogleRestRequestsAdapter
    {
        public void ExchangeAuthCodeWithIdToken(Dictionary<string, string> requestParams,
            TaskCompletionSource<GoogleIdTokenResponse> tcs);

        public void RefreshAccessToken(Dictionary<string, string> requestParams,
            TaskCompletionSource<GoogleRefreshTokenResponse> tcs);
    }
}