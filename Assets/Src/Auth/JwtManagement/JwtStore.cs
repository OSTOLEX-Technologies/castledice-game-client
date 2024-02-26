using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Src.Auth.JwtManagement.Tokens;

namespace Src.Auth.JwtManagement
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

        public JwtStore(IJwtToken accessToken, IJwtToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}