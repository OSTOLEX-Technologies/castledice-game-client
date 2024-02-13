using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider;
using Src.Auth.REST;
using Src.Auth.REST.REST_Request_Proxies.Metamask;
using Src.Auth.REST.REST_Response_DTOs.MetamaskBackend;

namespace Tests.EditMode.AuthTests.RestRequestsAdapters
{
    public class MetamaskRestRequestsAdapterTests
    {
        private static string[] testStrings =
        {
            "asldkjfahsd",
            "eoirt34w5r",
            "2346539485y"
        };
        
        [Test]
        public async Task GetNonce_ShouldObtainNonce([ValueSource(nameof(testStrings))] string address)
        {
            var expectedNonceResponse = new MetamaskNonceResponse();
            var nonceRequestParamsStub = new MetamaskNonceRequestDtoProxy(address);
            
            var httpClientRequestAdapterMock = new Mock<IHttpClientRequestAdapter>();
            var urlProviderMock = new Mock<IMetamaskBackendUrlProvider>();

            httpClientRequestAdapterMock.Setup(a => a.Request<MetamaskNonceResponse>(
                HttpMethod.Get,
                It.IsAny<string>(),
                nonceRequestParamsStub.AsDictionary()
            )).ReturnsAsync(expectedNonceResponse);

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object, urlProviderMock.Object);
            var res = await restRequestsAdapter.GetNonce(nonceRequestParamsStub);

            Assert.AreEqual(expectedNonceResponse, res);
        }
        
        [Test]
        public async Task AuthenticateAndGetTokens_ShouldObtainTokens(
            [ValueSource(nameof(testStrings))] string address,
            [ValueSource(nameof(testStrings))] string message)
        {
            var expectedAuthResponse = new MetamaskAccessTokenResponse();
            var authRequestParamsStub = new MetamaskAuthRequestDtoProxy(address, message);
            
            var httpClientRequestAdapterMock = new Mock<IHttpClientRequestAdapter>();
            var urlProviderMock = new Mock<IMetamaskBackendUrlProvider>();

            httpClientRequestAdapterMock.Setup(a => a.Request<MetamaskAccessTokenResponse>(
                HttpMethod.Get,
                It.IsAny<string>(),
                authRequestParamsStub.AsDictionary()
            )).ReturnsAsync(expectedAuthResponse);

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object, urlProviderMock.Object);
            var res = await restRequestsAdapter.AuthenticateAndGetTokens(authRequestParamsStub);

            Assert.AreEqual(expectedAuthResponse, res);
        }
        
        [Test]
        public async Task RefreshAccessTokens_ShouldObtainRefreshedTokens([ValueSource(nameof(testStrings))] string token)
        {
            var expectedRefreshResponse = new MetamaskRefreshTokenResponse();
            var authRequestParamsStub = new MetamaskRefreshRequestDtoProxy(token);
            
            var httpClientRequestAdapterMock = new Mock<IHttpClientRequestAdapter>();
            var urlProviderMock = new Mock<IMetamaskBackendUrlProvider>();

            httpClientRequestAdapterMock.Setup(a => a.Request<MetamaskRefreshTokenResponse>(
                HttpMethod.Get,
                It.IsAny<string>(),
                authRequestParamsStub.AsDictionary()
            )).ReturnsAsync(expectedRefreshResponse);

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object, urlProviderMock.Object);
            var res = await restRequestsAdapter.RefreshAccessTokens(authRequestParamsStub);

            Assert.AreEqual(expectedRefreshResponse, res);
        }
    }
}