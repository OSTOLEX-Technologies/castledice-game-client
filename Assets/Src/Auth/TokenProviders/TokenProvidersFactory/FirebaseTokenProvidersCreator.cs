using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Src.Auth.CredentialProviders.Firebase;
using Src.Auth.Exceptions.Authorization;
using UnityEngine;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
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
        
        public async Task<FirebaseTokenProvider> GetTokenProviderAsync(AuthType authProviderType)
        {
            //FirebaseUser could be cached by Firebase SDK
            if (_auth.CurrentUser != null)
            {
                if (!_auth.CurrentUser.IsValid())
                {
                    throw new AuthFailedException();
                }
                return new FirebaseTokenProvider(_auth.CurrentUser);
            }

            try
            {
                var credentials = await _firebaseCredentialProvider.GetCredentialAsync(authProviderType);
                var user = await _auth.SignInAndRetrieveDataWithCredentialAsync(credentials);
                return new FirebaseTokenProvider(user.User);
            }
            catch(FirebaseException e)
            {
                throw new AuthUnhandledException(e.Message);
            }
        }
    }
}