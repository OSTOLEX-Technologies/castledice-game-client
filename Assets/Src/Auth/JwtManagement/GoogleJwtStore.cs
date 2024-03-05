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
        
        public override bool Equals(object obj)
        {
            if (obj is not GoogleJwtStore other) return false;
            return 
                base.Equals(obj) &&
                idToken.Equals(other.idToken);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(accessToken, refreshToken, idToken);
        }
    }
}