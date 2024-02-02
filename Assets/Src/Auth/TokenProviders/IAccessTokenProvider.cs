using System.Threading.Tasks;

namespace Src.Auth.TokenProviders
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}