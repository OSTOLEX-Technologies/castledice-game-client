namespace Src.AuthController.JwtManagement.Tokens
{
    public interface IJwtToken
    {
        public bool Valid { get; }
        public string GetToken();

        public void UpdateToken(string token);
    }
}