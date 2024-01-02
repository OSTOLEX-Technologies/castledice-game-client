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
        public async Task GetAccessTokenAsync_ShouldObtainCorrectToken([Random(1000000, 10000000, 5)] int tokenStub)
        {
            var credentialProviderMock = new Mock<IMetamaskBackendCredentialProvider>();
            credentialProviderMock.Setup(a => a.GetCredentialAsync())
                .ReturnsAsync(new MetamaskAccessTokenResponse(tokenStub.ToString()));

            var metamaskTokenProvider = new MetamaskTokenProvider(credentialProviderMock.Object);
            var res = await metamaskTokenProvider.GetAccessTokenAsync();
            
            Assert.AreEqual(tokenStub.ToString(), res);
        }
    }
}