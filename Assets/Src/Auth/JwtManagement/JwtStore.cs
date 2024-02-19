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
        public JwtToken accessToken;
        
        [JsonProperty]
        [JsonPropertyName("refresh_token")]
        public JwtToken refreshToken;

        public JwtStore(JwtToken accessToken, JwtToken refreshToken)
        {
            this.accessToken = accessToken;
            this.refreshToken = refreshToken;
        }
    }
}