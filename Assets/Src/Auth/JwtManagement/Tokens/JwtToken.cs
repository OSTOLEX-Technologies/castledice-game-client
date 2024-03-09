using System;
using Newtonsoft.Json;

namespace Src.Auth.JwtManagement.Tokens
{
    [Serializable]
    public class JwtToken : IJwtToken
    {
        public bool Valid => DateTime.Now < _issuedAt + TimeSpan.FromSeconds(_expiresInSeconds);
        public string Token => _token;
        

        [JsonProperty("token")] private string _token;

        [JsonProperty("expires_in")] private int _expiresInSeconds;

        [JsonProperty("issued_at")] private DateTime _issuedAt;

        public JwtToken(string token, int expiresInSeconds, DateTime issuedAt)
        {
            _token = token;
            _expiresInSeconds = expiresInSeconds;
            _issuedAt = issuedAt;
        }

        public void UpdateToken(string token)
        {
            _token = token;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is not JwtToken other) return false;
            return 
                _token == other._token && 
                _expiresInSeconds == other._expiresInSeconds && 
                _issuedAt.Equals(other._issuedAt);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_token, _expiresInSeconds, _issuedAt);
        }
    }
}