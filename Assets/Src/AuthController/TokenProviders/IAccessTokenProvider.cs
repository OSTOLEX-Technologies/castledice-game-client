using System.Threading.Tasks;

namespace Src.AuthController.TokenProviders
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}