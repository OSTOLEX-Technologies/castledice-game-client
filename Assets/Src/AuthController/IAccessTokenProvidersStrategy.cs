namespace Src.AuthController
{
    public interface IAccessTokenProvidersStrategy
    {
        IAccessTokenProvider GetAccessTokenProvider(AuthType authType);
    }
}