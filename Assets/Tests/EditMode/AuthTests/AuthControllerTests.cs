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
        [TestCase(AuthType.Google)]
        [TestCase(AuthType.Metamask)]
        public void CacheObject_ShouldSubscribeToAuthRequestEvent(AuthType authType)
        {
            var providersStrategyMock = new Mock<IAccessTokenProvidersStrategy>();
            AuthViewMock viewMock = new AuthViewMock();
            
            var controller = new AuthControllerMock(providersStrategyMock.Object, new SingletonCacher(), viewMock);
            
            viewMock.SelectAuthType(authType);
            Assert.True(controller.EventFired);
        }
        
        [Test]
        [TestCase(AuthType.Google, typeof(FirebaseTokenProvider))]
        [TestCase(AuthType.Metamask, typeof(MetamaskTokenProvider))]
        public void OnAuthTypeChosen_ShouldCacheAccessTokenProvider(AuthType authType, object tokenProviderClass)
        {
            var providersStrategyMock = new Mock<IAccessTokenProvidersStrategy>();
            AuthViewMock viewMock = new AuthViewMock();
            
            var controller = new AuthController(providersStrategyMock.Object, new SingletonCacher(), viewMock);

            Assert.IsInstanceOf<IAccessTokenProvider>(Singleton<IAccessTokenProvider>.Instance);
        }
        
        [TearDown]
        public void UnregisterTokenProviderSingleton()
        {
            if (Singleton<IAccessTokenProvider>.Registered)
            {
                Singleton<IAccessTokenProvider>.Unregister();
            }
        }
    }
}