using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Src.Auth.JwtManagement.Tokens;

namespace Src.Auth.JwtManagement
{
    [Serializable]
    public class JwtStore : AbstractJwtStore
    {
        [JsonProperty]
        [JsonPropertyName("access_token")]
        public JwtToken accessToken;
        
        [JsonProperty]
        [JsonPropertyName("refresh_token")]
        public JwtToken refreshToken;

        public JwtStore(JwtToken accessToken, JwtToken refreshToken)
        {
            Type = JwtStoreType.Standard;
            
            this.accessToken = accessToken;
            this.refreshToken = refreshToken;
        }

        public override bool Equals(object obj)
        {
            if (obj is not JwtStore other) return false;
            return 
                accessToken.Equals(other.accessToken) &&
                refreshToken.Equals(other.refreshToken);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(accessToken, refreshToken);
        }
    }
}