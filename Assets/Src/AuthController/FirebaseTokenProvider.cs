using System.Threading.Tasks;

namespace Src.AuthController
{
    public class FirebaseTokenProvider : IAccessTokenProvider
    {
        public Task<string> GetAccessTokenAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}