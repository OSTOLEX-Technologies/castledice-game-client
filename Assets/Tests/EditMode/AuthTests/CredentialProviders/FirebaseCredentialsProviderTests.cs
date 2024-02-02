using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Moq;
using NUnit.Framework;
using Src.Auth;
using Src.Auth.CredentialProviders.Firebase;
using Src.Auth.CredentialProviders.Firebase.Google;
using Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.Auth.JwtManagement;
using Src.Auth.JwtManagement.Tokens;

namespace Tests.EditMode.AuthTests.CredentialProviders
{
    public class FirebaseCredentialsProviderTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public async Task GetCredentialAsync_ShouldGetValidCredentials(FirebaseAuthProviderType firebaseAuthProviderType)
        {
            var googleCredentials = new GoogleJwtStore(
                new JwtToken("id", Int32.MaxValue, DateTime.Now),
                new JwtToken("access", Int32.MaxValue, DateTime.Now),
                new JwtToken("refresh", Int32.MaxValue, DateTime.Now)
                );
            var expectedCredentials = new Credential();

            //Test object
            FirebaseCredentialProvider firebaseCredentialProvider = null;
            
            switch (firebaseAuthProviderType)
            {
                case FirebaseAuthProviderType.Google:
                    var googleCredentialProviderMock = new Mock<IGoogleCredentialProvider>();
                    googleCredentialProviderMock.Setup(
                        a => a.GetCredentialAsync()).ReturnsAsync(googleCredentials);
                    var firebaseInternalCredentialProviderFactoryMock = new Mock<IFirebaseInternalCredentialProviderCreator>();
                    firebaseInternalCredentialProviderFactoryMock.Setup(
                        a => a.CreateGoogleCredentialProvider()).Returns(googleCredentialProviderMock.Object);
                    var firebaseCredentialFormatterMock =
                        new Mock<IFirebaseCredentialFormatter>();
                    firebaseCredentialFormatterMock.Setup(
                        a => a.FormatCredentials(googleCredentials)).Returns(expectedCredentials);
            
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