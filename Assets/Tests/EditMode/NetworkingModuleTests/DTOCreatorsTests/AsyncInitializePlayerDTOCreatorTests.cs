using System;
using Moq;
using NUnit.Framework;
using Src.Auth.TokenProviders;
using Src.NetworkingModule.DTOCreators;

namespace Tests.EditMode.NetworkingModuleTests.DTOCreatorsTests
{
    public class AsyncInitializePlayerDTOCreatorTests
    {
        [Test]
        public void GetDTOAsync_ShouldReturnInitializePlayerDTO_WithAccessTokenFromProvider()
        {
            var expectedToken = new Random().Next().ToString();
            var accessTokenProviderMock = new Mock<IAccessTokenProvider>();
            accessTokenProviderMock.Setup(provider => provider.GetAccessTokenAsync()).ReturnsAsync(expectedToken);
            var dtoCreator = new InitializePlayerDtoCreator(accessTokenProviderMock.Object);
            
            var actualDTO = dtoCreator.CreateAsync().Result;
            
            Assert.AreEqual(expectedToken, actualDTO.VerificationKey);
        }
    }
}