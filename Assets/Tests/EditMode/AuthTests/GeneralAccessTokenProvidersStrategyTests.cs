using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController;
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
            var expectedFirebaseTokenProvider = new Mock<FirebaseTokenProvider>();
            var expectedMetamaskTokenProvider = new Mock<MetamaskTokenProvider>();

            var firebaseTokenProviderFactoryMock = new Mock<IFirebaseTokenProvidersFactory>();
            firebaseTokenProviderFactoryMock.Setup(s => 
                s.GetTokenProviderAsync(FirebaseAuthProviderType.Google)).ReturnsAsync(expectedFirebaseTokenProvider.Object);
            var metamaskTokenProviderFactoryMock = new Mock<IMetamaskTokenProvidersFactory>();
            metamaskTokenProviderFactoryMock.Setup(s => 
                s.GetTokenProviderAsync()).ReturnsAsync(expectedMetamaskTokenProvider.Object);

            
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