using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.Auth.TokenProviders;
using Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameSearchingTests.CancelRequestingTests
{
    public class CancelGameDtoCreatorTests
    {
        [Test]
        public async Task CreateDtoAsync_ShouldReturnDto_WithToken_FromProvider()
        {
            var token = new Random().Next().ToString();
            var providerMock = new Mock<IAccessTokenProvider>();
            providerMock.Setup(provider => provider.GetAccessTokenAsync()).ReturnsAsync(token);
            var creator = new CancelGameDtoCreator(providerMock.Object);
            
            var dto = await creator.CreateDtoAsync();
            
            Assert.AreEqual(token, dto.VerificationKey);
        }
    }
}