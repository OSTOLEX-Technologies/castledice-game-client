using System.Threading.Tasks;

namespace Src.AuthController
{
    public class MetamaskTokenProvider : IAccessTokenProvider
    {
        public Task<string> GetAccessTokenAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}