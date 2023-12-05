using System;

namespace Src.AuthController.REST.REST_Response_DTOs
{
    [Serializable]
    public class GoogleIdTokenResponse
    {
        public string access_token;

        //In seconds
        public string expires_in;
        
        public string id_token;

        public string refresh_token;
        
        public string scope;
        
        public string token_type;
    }
}