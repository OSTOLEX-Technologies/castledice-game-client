using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Src.AuthController.JwtManagement.Tokens;

namespace Src.AuthController.JwtManagement
{
    [Serializable]
    public class GoogleJwtStore : JwtStore
    {
        [JsonProperty]
        [JsonPropertyName("id_token")]
        public IJwtToken IdToken;

        public GoogleJwtStore(IJwtToken idToken, IJwtToken accessToken, IJwtToken refreshToken) : base(accessToken, refreshToken)
        {
            IdToken = IdToken;
        }
    }
}