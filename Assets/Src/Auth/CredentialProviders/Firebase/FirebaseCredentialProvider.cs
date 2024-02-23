using System.Threading.Tasks;
using Firebase.Auth;
using Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.Auth.Exceptions;

namespace Src.Auth.CredentialProviders.Firebase
{
    public class FirebaseCredentialProvider : IFirebaseCredentialProvider
    {
        private readonly IFirebaseInternalCredentialProviderCreator _internalCredentialProviderCreator;
        private readonly IFirebaseCredentialFormatter _firebaseCredentialFormatter;

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
                    var googleCredentialProvider = _internalCredentialProviderCreator.CreateGoogleCredentialProvider();
                    var googleCredentials = await googleCredentialProvider.GetCredentialAsync();
                    var credential = _firebaseCredentialFormatter.FormatCredentials(googleCredentials);
                    
                    return credential;
                
                default: 
                    throw new FirebaseCredentialProviderNotFoundException(authProviderType);
            }
        }
    }
}