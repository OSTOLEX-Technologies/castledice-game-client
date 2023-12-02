using System;

namespace Src.AuthController.REST.REST_Responses
{
    [Serializable]
    public class GoogleIdTokenResponse
    {
        public string access_token;

        //In seconds
        public string expires_in;
        
        public string id_token;

        public string refresh_token;
    }
}