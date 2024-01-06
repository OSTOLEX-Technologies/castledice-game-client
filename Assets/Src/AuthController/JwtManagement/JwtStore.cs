using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Src.AuthController.JwtManagement.Tokens;

namespace Src.AuthController.JwtManagement
{
    [Serializable]
    public class JwtStore
    {
        [JsonProperty]
        [JsonPropertyName("access_token")]
        public IJwtToken AccessToken;
        
        [JsonProperty]
        [JsonPropertyName("refresh_token")]
        public IJwtToken RefreshToken;

        public JwtStore()
        {
            AccessToken = new JwtToken();
            RefreshToken = new JwtToken();
        }

        public JwtStore(IJwtToken accessToken, IJwtToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}