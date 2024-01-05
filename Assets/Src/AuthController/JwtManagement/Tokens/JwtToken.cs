using System;
using Newtonsoft.Json;

namespace Src.AuthController.JwtManagement.Tokens
{
    [Serializable]
    public class JwtToken : IJwtToken
    {
        public bool Valid => DateTime.Now < IssuedAt + TimeSpan.FromSeconds(ExpiresInSeconds);

        [JsonProperty("token")] public string Token { get; private set; }

        [JsonProperty("expires_in")] public int ExpiresInSeconds { get; private set; }

        [JsonProperty("issued_at")] public DateTime IssuedAt { get; private set; }


        public JwtToken(string token, int expiresInSeconds, DateTime issuedAt)
        {
            Token = token;
            ExpiresInSeconds = expiresInSeconds;
            IssuedAt = issuedAt;
        }

        public string GetToken()
        {
            return Token;
        }

        public void UpdateToken(string token)
        {
            Token = token;
        }
    }
}