using System.Threading.Tasks;
using Firebase.Auth;
using Src.Auth.CredentialProviders.Firebase.Google;
using Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.Auth.Exceptions;

namespace Src.Auth.CredentialProviders.Firebase
{
    public class FirebaseCredentialProvider : IFirebaseCredentialProvider
    {
        private readonly IFirebaseInternalCredentialProviderCreator _internalCredentialProviderCreator;
        private readonly IFirebaseCredentialFormatter _firebaseCredentialFormatter;

        private IGoogleCredentialProvider _googleCredentialProvider;

        public FirebaseCredentialProvider(
            IFirebaseInternalCredentialProviderCreator internalCredentialProviderCreator,
            IFirebaseCredentialFormatter firebaseCredentialFormatter)
        {
            _internalCredentialProviderCreator = internalCredentialProviderCreator;
            _firebaseCredentialFormatter = firebaseCredentialFormatter;
        }
        
        public async Task<Credential> GetCredentialAsync(AuthType authProviderType)
        {
            switch (authProviderType)
            {
                case AuthType.Google:
                    _googleCredentialProvider = _internalCredentialProviderCreator.CreateGoogleCredentialProvider();
                    var googleCredentials = await _googleCredentialProvider.GetCredentialAsync();
                    var credential = _firebaseCredentialFormatter.FormatCredentials(googleCredentials);
                    
                    return credential;
                
                default: 
                    throw new FirebaseCredentialProviderNotFoundException(authProviderType);
            }
        }

        public void InterruptGoogleProviderInit()
        {
            _googleCredentialProvider?.InterruptProviderInit();
        }
    }
}