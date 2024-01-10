﻿using System.Threading.Tasks;
using Firebase.Auth;
using Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.AuthController.Exceptions;

namespace Src.AuthController.CredentialProviders.Firebase
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
        
        public async Task<Credential> GetCredentialAsync(FirebaseAuthProviderType authProviderType)
        {
            switch (authProviderType)
            {
                case FirebaseAuthProviderType.Google:
                    var googleCredentialProvider = _internalCredentialProviderCreator.CreateGoogleCredentialProvider();
                    var googleCredentials = await googleCredentialProvider.GetCredentialAsync();
                    Credential credential = _firebaseCredentialFormatter.FormatCredentials(googleCredentials);
                    return credential;
                default:
                    throw new FirebaseCredentialProviderNotFoundException(authProviderType);
            }
        }
    }
}