using System.Threading.Tasks;
using Firebase.Auth;
using Src.AuthController.CredentialProviders.Firebase;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public class FirebaseTokenProvidersCreator : IFirebaseTokenProvidersCreator
    {
        private readonly IFirebaseCredentialProvider _firebaseCredentialProvider;
        private readonly FirebaseAuth _auth;
        public FirebaseTokenProvidersCreator(IFirebaseCredentialProvider firebaseCredentialProvider)
        {
            _firebaseCredentialProvider = firebaseCredentialProvider;
            _auth = FirebaseAuth.DefaultInstance;
        }
        
        public async Task<FirebaseTokenProvider> GetTokenProviderAsync(FirebaseAuthProviderType authProviderType)
        {
            var credentials = await _firebaseCredentialProvider.GetCredentialAsync(authProviderType);
            //var user = await _auth.SignInAndRetrieveDataWithCredentialAsync(credentials);
            // return new FirebaseTokenProvider(user.User);
            return new FirebaseTokenProvider();
        }
    }
}