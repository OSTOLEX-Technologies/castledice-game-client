using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController.CredentialProviders.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;
using Src.AuthController.TokenProviders;

namespace Tests.EditMode.AuthTests.TokenProviders
{
    public class MetamaskTokenProviderTests
    {
        [Test]
        public async Task GetAccessTokenAsync_ShouldObtainCorrectToken()
        {
            var expectedAccessTokenResponse = new MetamaskAccessTokenResponse();
            var credentialProviderMock = new Mock<IMetamaskBackendCredentialProvider>();
            credentialProviderMock.Setup(a => a.GetCredentialAsync())
                .ReturnsAsync(expectedAccessTokenResponse);

            var metamaskTokenProvider = new MetamaskTokenProvider(credentialProviderMock.Object);
            var res = await metamaskTokenProvider.GetAccessTokenAsync();
            
            Assert.AreEqual(expectedAccessTokenResponse.AccessToken, res);
        }
    }
}