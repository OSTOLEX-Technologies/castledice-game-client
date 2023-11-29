using System.Threading.Tasks;

namespace Src.AuthController
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync(AuthType authType);
    }
}