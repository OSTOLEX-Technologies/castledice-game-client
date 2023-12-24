using System.Threading.Tasks;

namespace Src.AuthController.TokenProviders
{
    public class MetamaskTokenProvider : IAccessTokenProvider
    {
        public Task<string> GetAccessTokenAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}