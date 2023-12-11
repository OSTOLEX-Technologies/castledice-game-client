using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Moq;
using NUnit.Framework;
using Src.AuthController;
using Src.AuthController.CredentialProviders.Firebase;
using Src.AuthController.CredentialProviders.Firebase.Google;
using Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.AuthController.REST.REST_Response_DTOs;

namespace Tests.EditMode.AuthTests
{
    public class FirebaseCredentialsProviderTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public async Task GetCredentialAsync_ShouldGetValidCredentials(FirebaseAuthProviderType firebaseAuthProviderType)
        {
            var googleIdResponse = new GoogleIdTokenResponse();
            var expectedCredentials = new Credential();

            //Test object
            FirebaseCredentialProvider firebaseCredentialProvider = null;
            
            switch (firebaseAuthProviderType)
            {
                case FirebaseAuthProviderType.Google:
                    var googleCredentialProviderMock = new Mock<IGoogleCredentialProvider>();
                    googleCredentialProviderMock.Setup(
                        a => a.GetCredentialAsync()).ReturnsAsync(googleIdResponse);
                    var firebaseInternalCredentialProviderFactoryMock = new Mock<IFirebaseInternalCredentialProviderFactory>();
                    firebaseInternalCredentialProviderFactoryMock.Setup(
                        a => a.CreateGoogleCredentialProvider()).Returns(googleCredentialProviderMock.Object);
                    var firebaseCredentialFormatterMock =
                        new Mock<IFirebaseCredentialFormatter>();
                    firebaseCredentialFormatterMock.Setup(
                        a => a.FormatCredentials(googleIdResponse)).Returns(expectedCredentials);
            
                    firebaseCredentialProvider = new FirebaseCredentialProvider(
                        firebaseInternalCredentialProviderFactoryMock.Object, 
                        firebaseCredentialFormatterMock.Object);
                    
                    break;
            }

            var resultCredentials = await firebaseCredentialProvider.GetCredentialAsync(firebaseAuthProviderType);
            Assert.AreSame(expectedCredentials, resultCredentials);
        }
            
        public static IEnumerable<FirebaseAuthProviderType> GetAuthTypes()
        {
            var authTypes = Enum.GetValues(typeof(FirebaseAuthProviderType));
            foreach (var firebaseAuthType in authTypes) 
            {
                yield return (FirebaseAuthProviderType) firebaseAuthType;
            }
        }
    }
}