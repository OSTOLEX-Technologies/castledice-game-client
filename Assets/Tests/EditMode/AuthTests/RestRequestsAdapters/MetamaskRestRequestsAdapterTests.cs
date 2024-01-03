using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.AuthController.REST;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

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
            var tcs = new TaskCompletionSource<MetamaskNonceResponse>();

            httpClientRequestAdapterMock.Setup(a => a.Request(
                HttpMethod.Get,
                It.IsAny<string>(),
                nonceRequestParamsStub.AsDictionary(),
                tcs
            )).Callback<HttpMethod, string, Dictionary<string, string>, TaskCompletionSource<MetamaskNonceResponse>>
            ((_, _, _, tcs) => tcs.SetResult(expectedNonceResponse));

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object);
            restRequestsAdapter.GetNonce(nonceRequestParamsStub, tcs);

            var res = await tcs.Task;

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
            var tcs = new TaskCompletionSource<MetamaskAccessTokenResponse>();
            
            httpClientRequestAdapterMock.Setup(a => a.Request(
                HttpMethod.Get,
                It.IsAny<string>(),
                authRequestParamsStub.AsDictionary(),
                tcs
            )).Callback<HttpMethod, string, Dictionary<string, string>, TaskCompletionSource<MetamaskAccessTokenResponse>>
                ((_, _, _, tcs) => tcs.SetResult(expectedAuthResponse));

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object);
            restRequestsAdapter.AuthenticateAndGetTokens(authRequestParamsStub, tcs);
            
            var res = await tcs.Task;
            
            Assert.AreEqual(expectedAuthResponse, res);
        }
        
        [Test]
        public async Task RefreshAccessTokens_ShouldObtainRefreshedTokens([ValueSource(nameof(testStrings))] string token)
        {
            var expectedRefreshResponse = new MetamaskRefreshTokenResponse();
            var authRequestParamsStub = new MetamaskRefreshRequestDtoProxy(token);
            
            var httpClientRequestAdapterMock = new Mock<IHttpClientRequestAdapter>();
            var tcs = new TaskCompletionSource<MetamaskRefreshTokenResponse>();
            
            httpClientRequestAdapterMock.Setup(a => a.Request(
                HttpMethod.Get,
                It.IsAny<string>(),
                authRequestParamsStub.AsDictionary(),
                tcs
            )).Callback<HttpMethod, string, Dictionary<string, string>, TaskCompletionSource<MetamaskRefreshTokenResponse>>
                ((_, _, _, tcs) => tcs.SetResult(expectedRefreshResponse));

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object);
            restRequestsAdapter.RefreshAccessTokens(authRequestParamsStub, tcs);
            
            var res = await tcs.Task;
            
            Assert.AreEqual(expectedRefreshResponse, res);
        }
    }
}