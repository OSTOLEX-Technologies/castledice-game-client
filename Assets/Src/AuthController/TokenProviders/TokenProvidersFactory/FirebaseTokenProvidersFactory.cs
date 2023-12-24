using System.Threading.Tasks;
using Firebase.Auth;
using Src.AuthController.CredentialProviders.Firebase;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public class FirebaseTokenProvidersFactory : IFirebaseTokenProvidersFactory
    {
        private readonly IFirebaseCredentialProvider _credentialProvider;
        private readonly FirebaseAuth _auth;
        public FirebaseTokenProvidersFactory(IFirebaseCredentialProvider credentialProvider)
        {
            _credentialProvider = credentialProvider;
            _auth = FirebaseAuth.DefaultInstance;
        }
        
        public async Task<FirebaseTokenProvider> GetTokenProviderAsync(FirebaseAuthProviderType authProviderType)
        {
            var credentials = await _credentialProvider.GetCredentialAsync(authProviderType);
            var user = await _auth.SignInAndRetrieveDataWithCredentialAsync(credentials);
            return new FirebaseTokenProvider(user.User);
        }
    }
}