using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.Auth;
using Src.Auth.TokenProviders;
using Src.Auth.TokenProviders.TokenProvidersFactory;
using Src.General.Caching;

namespace Tests.EditMode.AuthTests
{
    public class AuthControllerTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void OnAuthTypeChosen_ShouldCacheAccessTokenProvider_ObtainedFromStrategy(AuthType authType)
        {
            var usedTokenProvider = new Mock<IAccessTokenProvider>().Object;
            var providersStrategyMock = new Mock<IAccessTokenProvidersStrategy>();
            providersStrategyMock.Setup(s => s.GetAccessTokenProviderAsync(authType)).ReturnsAsync(usedTokenProvider);
            
            var cacherMock = new Mock<IObjectCacher>();
            var authViewMock = new Mock<IAuthView>();

            var controller = new AuthController(providersStrategyMock.Object, cacherMock.Object, authViewMock.Object);
            authViewMock.Raise(x => x.AuthTypeChosen += null, authType);
            authViewMock.Object.Login(authType);

            cacherMock.Verify(
                x => x.CacheObject(usedTokenProvider), 
                Times.Once);
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