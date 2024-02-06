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
        public IJwtToken IdToken;

        public GoogleJwtStore(IJwtToken idToken, IJwtToken accessToken, IJwtToken refreshToken) : base(accessToken, refreshToken)
        {
            IdToken = idToken;
        }
    }
}