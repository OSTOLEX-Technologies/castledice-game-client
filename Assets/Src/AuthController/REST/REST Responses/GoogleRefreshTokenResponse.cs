using System;

namespace Src.AuthController.REST.REST_Responses
{
    [Serializable]
    public class GoogleRefreshTokenResponse
    {
        public string access_token;

        //In seconds
        public string expires_in;

        public string scope;

        public string token_type;
    }
}