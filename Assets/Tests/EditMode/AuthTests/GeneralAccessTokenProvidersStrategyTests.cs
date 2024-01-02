using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Moq;
using NUnit.Framework;
using Src.AuthController;
using Src.AuthController.CredentialProviders.Metamask;
using Src.AuthController.TokenProviders;
using Src.AuthController.TokenProviders.TokenProvidersFactory;

namespace Tests.EditMode.AuthTests
{
    public class GeneralAccessTokenProvidersStrategyTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public async Task GetAccessTokenProvider_ShouldReturnCorrectTokenProvider_ObtainedFromStrategy(AuthType authType)
        {
            var firebaseUserStub = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance).CurrentUser;
            var metamaskBackendCredentialsProviderMock = new Mock<IMetamaskBackendCredentialProvider>();
            
            var expectedFirebaseTokenProvider = new FirebaseTokenProvider(firebaseUserStub);
            var expectedMetamaskTokenProvider = new MetamaskTokenProvider(metamaskBackendCredentialsProviderMock.Object);

            var firebaseTokenProviderFactoryMock = new Mock<IFirebaseTokenProvidersFactory>();
            firebaseTokenProviderFactoryMock.Setup(s => 
                s.GetTokenProviderAsync(FirebaseAuthProviderType.Google)).ReturnsAsync(expectedFirebaseTokenProvider);
            var metamaskTokenProviderFactoryMock = new Mock<IMetamaskTokenProvidersFactory>();
            metamaskTokenProviderFactoryMock.Setup(s => 
                s.GetTokenProviderAsync()).ReturnsAsync(expectedMetamaskTokenProvider);

            
            var generalProviderStrategy = new GeneralAccessTokenProvidersStrategy(firebaseTokenProviderFactoryMock.Object, metamaskTokenProviderFactoryMock.Object);
            var actualTokenProvider = await generalProviderStrategy.GetAccessTokenProviderAsync(authType);
            
            
            if (authType.Equals(AuthType.Metamask))
            {
                Assert.AreSame(expectedMetamaskTokenProvider, actualTokenProvider);
            }
            else
            {
                Assert.AreSame(expectedFirebaseTokenProvider, actualTokenProvider);
            }
        }

        public static IEnumerable<AuthType> GetAuthTypes()
        {
            var authTypes = Enum.GetValues(typeof(AuthType));
            foreach (var authType in authTypes) 
            {
                yield return (AuthType) authType;
            }
        }
    }
}