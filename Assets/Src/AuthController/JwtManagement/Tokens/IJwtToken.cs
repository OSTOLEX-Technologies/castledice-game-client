namespace Src.AuthController.JwtManagement.Tokens
{
    public interface IJwtToken
    {
        public bool Valid { get; }
        
        public string Token { get; }

        public void UpdateToken(string token);
    }
}