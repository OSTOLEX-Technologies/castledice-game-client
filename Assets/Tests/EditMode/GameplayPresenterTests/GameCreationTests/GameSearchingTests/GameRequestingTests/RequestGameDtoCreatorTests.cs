using System;
using Moq;
using NUnit.Framework;
using Src.Auth.TokenProviders;
using Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameSearchingTests.GameRequestingTests
{
    public class RequestGameDtoCreatorTests
    {
        [Test]
        public void CreateDtoAsync_ShouldReturnDto_WithToken_FromProvider()
        {
            var expectedToken = new Random().Next().ToString();
            var providerMock = new Mock<IAccessTokenProvider>();
            providerMock.Setup(provider => provider.GetAccessTokenAsync()).ReturnsAsync(expectedToken);
            var creator = new RequestGameDtoCreator(providerMock.Object);
            
            var dto = creator.CreateDtoAsync().Result;
            
            Assert.AreEqual(expectedToken, dto.VerificationKey);
        }
    }
}