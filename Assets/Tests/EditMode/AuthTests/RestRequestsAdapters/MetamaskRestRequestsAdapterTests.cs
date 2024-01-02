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
        [Test]
        public async Task GetNonce_ShouldObtainNonce()
        {
            var expectedNonceResponse = new MetamaskNonceResponse();
            
            var httpClientRequestAdapterMock = new Mock<IHttpClientRequestAdapter>();
            var tcs = new TaskCompletionSource<MetamaskNonceResponse>();
            
            httpClientRequestAdapterMock.Setup(a => a.Request(
                HttpMethod.Get,
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<TaskCompletionSource<MetamaskNonceResponse>>()
            )).Callback<HttpMethod, string, Dictionary<string, string>, TaskCompletionSource<MetamaskNonceResponse>>
                ((_, _, _, tcs) => tcs.SetResult(expectedNonceResponse));

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object);
            restRequestsAdapter.GetNonce(tcs);
            var res = await tcs.Task;
            
            Assert.AreEqual(expectedNonceResponse, res);
        }
        
        [Test]
        public async Task AuthenticateAndGetTokens_ShouldObtainTokens()
        {
            var expectedAuthResponse = new MetamaskAccessTokenResponse();
            var authRequestParamsStub = new MetamaskAuthRequestDtoProxy("");
            
            var httpClientRequestAdapterMock = new Mock<IHttpClientRequestAdapter>();
            var tcs = new TaskCompletionSource<MetamaskAccessTokenResponse>();
            
            httpClientRequestAdapterMock.Setup(a => a.Request(
                HttpMethod.Get,
                It.IsAny<string>(),
                authRequestParamsStub.AsDictionary(),
                It.IsAny<TaskCompletionSource<MetamaskAccessTokenResponse>>()
            )).Callback<HttpMethod, string, Dictionary<string, string>, TaskCompletionSource<MetamaskAccessTokenResponse>>
                ((_, _, _, tcs) => tcs.SetResult(expectedAuthResponse));

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object);
            restRequestsAdapter.AuthenticateAndGetTokens(authRequestParamsStub, tcs);
            var res = await tcs.Task;
            
            Assert.AreEqual(expectedAuthResponse, res);
        }
        
        [Test]
        public async Task RefreshAccessTokens_ShouldObtainRefreshedTokens()
        {
            var expectedRefreshResponse = new MetamaskRefreshTokenResponse();
            var authRequestParamsStub = new MetamaskRefreshRequestDtoProxy("");
            
            var httpClientRequestAdapterMock = new Mock<IHttpClientRequestAdapter>();
            var tcs = new TaskCompletionSource<MetamaskRefreshTokenResponse>();
            
            httpClientRequestAdapterMock.Setup(a => a.Request(
                HttpMethod.Get,
                It.IsAny<string>(),
                authRequestParamsStub.AsDictionary(),
                It.IsAny<TaskCompletionSource<MetamaskRefreshTokenResponse>>()
            )).Callback<HttpMethod, string, Dictionary<string, string>, TaskCompletionSource<MetamaskRefreshTokenResponse>>
                ((_, _, _, tcs) => tcs.SetResult(expectedRefreshResponse));

            var restRequestsAdapter = new MetamaskRestRequestsAdapter(httpClientRequestAdapterMock.Object);
            restRequestsAdapter.RefreshAccessTokens(authRequestParamsStub, tcs);
            var res = await tcs.Task;
            
            Assert.AreEqual(expectedRefreshResponse, res);
        }
    }
}