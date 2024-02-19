using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Src.Auth.JwtManagement.Tokens;

namespace Src.Auth.JwtManagement
{
    [Serializable]
    public class GoogleJwtStore : JwtStore
    {
        [JsonProperty]
        [JsonPropertyName("id_token")]
        public JwtToken idToken;

        public GoogleJwtStore(JwtToken idToken, JwtToken accessToken, JwtToken refreshToken) : base(accessToken, refreshToken)
        {
            this.idToken = idToken;
        }
    }
}