﻿using Src.AuthController.REST.REST_Response_DTOs;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;

namespace Src.AuthController.CredentialProviders.Firebase.Google.TokenValidator
{
    public interface IGoogleAccessTokenValidator
    {
        public bool ValidateAccessToken(GoogleIdTokenResponse googleIdTokenResponse, float googleApiResponseIssueTime);
    }
}