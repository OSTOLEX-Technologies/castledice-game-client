using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.AuthController;
using Src.Caching;
using Tests.Utils.Mocks;

namespace Tests.EditMode.AuthTests
{
    public class AuthControllerTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void OnAuthTypeChosen_ShouldCacheAccessTokenProvider_ObtainedFromStrategy(AuthType authType)
        {
            var expectedTokenProvider = new Mock<IAccessTokenProvider>().Object;
            var cacherMock = new Mock<IObjectCacher>();
            var providersStrategyMock = new Mock<IAccessTokenProvidersStrategy>();
            providersStrategyMock.Setup(s => s.GetAccessTokenProvider(authType)).Returns(expectedTokenProvider);
            AuthViewMock viewMock = new AuthViewMock();
            
            var controller = new AuthController(providersStrategyMock.Object, cacherMock.Object, viewMock);
            viewMock.SelectAuthType(authType);

            cacherMock.Verify(c => c.CacheObject(expectedTokenProvider), Times.Once);
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