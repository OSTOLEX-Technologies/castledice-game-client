using System.Threading.Tasks;
using Firebase.Auth;

namespace Src.Auth.TokenProviders
{
    public class FirebaseTokenProvider : IAccessTokenProvider
    {
        private readonly FirebaseUser _user;
        
        public FirebaseTokenProvider(FirebaseUser user)
        {
            _user = user;
        }
        
        public Task<string> GetAccessTokenAsync()
        {
            return _user.TokenAsync(true);
        }
    }
}