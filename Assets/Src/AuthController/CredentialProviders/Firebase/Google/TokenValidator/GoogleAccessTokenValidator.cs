using Src.AuthController.REST.REST_Response_DTOs;
using UnityEngine;

namespace Src.AuthController.CredentialProviders.Firebase.Google.TokenValidator
{
    public class GoogleAccessTokenValidator : IGoogleAccessTokenValidator
    {
        private const float AccessTokenValidityMargin = 30f;
        
        public bool ValidateAccessToken(GoogleIdTokenResponse googleIdTokenResponse, float googleApiResponseIssueTime)
        {
            int.TryParse(googleIdTokenResponse.expires_in, out var validityPeriod);

            return (Time.time - googleApiResponseIssueTime) <
                   (validityPeriod - AccessTokenValidityMargin);
        }
    }
}