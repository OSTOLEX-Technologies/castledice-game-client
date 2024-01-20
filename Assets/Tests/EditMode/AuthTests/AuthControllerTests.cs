using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.AuthController;
using Src.AuthController.TokenProviders;
using Src.AuthController.TokenProviders.TokenProvidersFactory;
using Src.Caching;

namespace Tests.EditMode.AuthTests
{
    public class AuthControllerTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void OnAuthTypeChosen_ShouldCacheAccessTokenProvider_ObtainedFromStrategy(AuthType authType)
        {
            bool bCached = false;
            
            var usedTokenProvider = new Mock<IAccessTokenProvider>().Object;
            var cacherMock = new Mock<IObjectCacher>();
            cacherMock.Setup(a => a.CacheObject(usedTokenProvider))
                .Callback<IAccessTokenProvider>((_) => bCached = true);
            var providersStrategyMock = new Mock<IAccessTokenProvidersStrategy>();
            providersStrategyMock.Setup(s => s.GetAccessTokenProviderAsync(authType)).ReturnsAsync(usedTokenProvider);
            var authViewMock = new Mock<IAuthView>();
            authViewMock.Setup(a => a.Login(authType)).Raises(a => a.AuthTypeChosen += null, this, authType);


            var controller = new AuthController(providersStrategyMock.Object, cacherMock.Object, authViewMock.Object);
            authViewMock.Object.Login(authType);

            Assert.IsTrue(bCached);
            //cacherMock.Verify(c => c.CacheObject(usedTokenProvider), Times.Once);
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