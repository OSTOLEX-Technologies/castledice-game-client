using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.AuthController;

namespace Tests.EditMode.AuthTests
{
    public class GeneralAccessTokenProvidersStrategyTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void GetAccessTokenProvider_ShouldReturnCorrectTokenProvider_ObtainedFromStrategy(AuthType authType)
        {
            var expectedFirebaseTokenProvider = new FirebaseTokenProvider();
            var expectedMetamaskTokenProvider = new MetamaskTokenProvider();

            var firebaseTokenProviderFactoryMock = new Mock<IFirebaseTokenProvidersFactory>();
            firebaseTokenProviderFactoryMock.Setup(s => 
                s.GetTokenProvider(FirebaseAuthProviderType.Google)).Returns(expectedFirebaseTokenProvider);
            var metamaskTokenProviderFactoryMock = new Mock<IMetamaskTokenProvidersFactory>();
            metamaskTokenProviderFactoryMock.Setup(s => 
                s.GetTokenProvider()).Returns(expectedMetamaskTokenProvider);

            
            var generalProviderStrategy = new GeneralAccessTokenProvidersStrategy(firebaseTokenProviderFactoryMock.Object, metamaskTokenProviderFactoryMock.Object);
            var actualTokenProvider = generalProviderStrategy.GetAccessTokenProvider(authType);
            
            
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