using System;
using System.Threading.Tasks;
using Firebase.Auth;

namespace Src.AuthController.TokenProviders
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
            throw new NotImplementedException();
        }
    }
}