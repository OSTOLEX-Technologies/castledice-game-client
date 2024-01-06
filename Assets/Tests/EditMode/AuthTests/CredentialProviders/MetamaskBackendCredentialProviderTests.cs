using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController.CredentialProviders.Metamask;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.AuthController.JwtManagement;
using Src.AuthController.JwtManagement.Converters.Metamask;
using Src.AuthController.JwtManagement.Tokens;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Tests.EditMode.AuthTests.CredentialProviders
{
    public class MetamaskBackendCredentialProviderTests
    {
        private class WalletFacadeMock : IMetamaskWalletFacade
        {
            private static string PublicAddressStub = "address";
            
            public void Connect()
            {
                OnConnected?.Invoke(this, EventArgs.Empty);
            }

            public string GetPublicAddress()
            {
                return PublicAddressStub;
            }

            public event EventHandler OnConnected;
        }
        
        private static string _validDeterminator = "valid";
        private static string _invalidDeterminator = "invalid";
        

        [Test]
        public async Task GetCredentialAsync_ShouldCreateCredentials_IfThereIsNoSavedAccessToken()
        {
            var usedResponse = new MetamaskAccessTokenResponse();

            var walletFacadeMock = new WalletFacadeMock();
            var signerMock = new Mock<IMetamaskSignerFacade>();
            signerMock.Setup(a => a.Sign(It.IsAny<string>())).ReturnsAsync("");

            var expectedMetamaskCredentials = new JwtStore(
                new JwtToken(_validDeterminator, Int32.MaxValue, DateTime.Now),
                new JwtToken("refresh", Int32.MaxValue, DateTime.Now)
            );

            var jwtConverterMock = new Mock<IMetamaskJwtConverter>();
            jwtConverterMock.Setup(a => a.FromMetamaskAuthResponse(usedResponse)).
                Returns(expectedMetamaskCredentials);
            
            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var metamaskRestRequestsAdapterMock = new Mock<IMetamaskRestRequestsAdapter>();
            metamaskRestRequestsAdapterMock.Setup(
                    a => a.GetNonce(
                        It.IsAny<MetamaskNonceRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<MetamaskNonceResponse>>()))
                .Callback<MetamaskNonceRequestDtoProxy, TaskCompletionSource<MetamaskNonceResponse>>(
                    (_, tcs) => tcs.SetResult(new MetamaskNonceResponse()));
            metamaskRestRequestsAdapterMock.Setup(
                    a => a.AuthenticateAndGetTokens(
                        It.IsAny<MetamaskAuthRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<MetamaskAccessTokenResponse>>()))
                .Callback<MetamaskAuthRequestDtoProxy, TaskCompletionSource<MetamaskAccessTokenResponse>>(
                    (_, tcs) => tcs.SetResult(usedResponse));

            var metamaskBackendCredentialProvider = new MetamaskBackendCredentialProvider(
                walletFacadeMock, 
                signerMock.Object,
                metamaskRestRequestsAdapterMock.Object,
                jwtConverterMock.Object);

            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(expectedMetamaskCredentials.AccessToken.GetToken(), result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldRefreshCredentials_IfThereExistsSavedExpiredAccessToken()
        {
            var usedResponse = new MetamaskAccessTokenResponse();
            var usedRefreshResponse = new MetamaskRefreshTokenResponse();

            var walletFacadeMock = new WalletFacadeMock();
            var signerMock = new Mock<IMetamaskSignerFacade>();
            signerMock.Setup(a => a.Sign(It.IsAny<string>())).ReturnsAsync("");

            var expectedMetamaskCredentials = new JwtStore(
                new JwtToken(_invalidDeterminator, -10, DateTime.Now),
                new JwtToken("refresh", Int32.MaxValue, DateTime.Now)
            );

            var jwtConverterMock = new Mock<IMetamaskJwtConverter>();
            jwtConverterMock.Setup(a => a.FromMetamaskAuthResponse(usedResponse)).
                Returns(expectedMetamaskCredentials);
            jwtConverterMock.Setup(a => a.FromMetamaskRefreshResponse(usedRefreshResponse)).
                Returns(expectedMetamaskCredentials);

            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var metamaskRestRequestsAdapterMock = new Mock<IMetamaskRestRequestsAdapter>();
            metamaskRestRequestsAdapterMock.Setup(
                    a => a.GetNonce(
                        It.IsAny<MetamaskNonceRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<MetamaskNonceResponse>>()))
                .Callback<MetamaskNonceRequestDtoProxy, TaskCompletionSource<MetamaskNonceResponse>>(
                    (_, tcs) => tcs.SetResult(new MetamaskNonceResponse()));
            metamaskRestRequestsAdapterMock.Setup(
                    a => a.AuthenticateAndGetTokens(
                        It.IsAny<MetamaskAuthRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<MetamaskAccessTokenResponse>>()))
                .Callback<MetamaskAuthRequestDtoProxy, TaskCompletionSource<MetamaskAccessTokenResponse>>(
                    (_, tcs) => tcs.SetResult(usedResponse));
            metamaskRestRequestsAdapterMock.Setup(
                a => a.RefreshAccessTokens(
                    It.IsAny<MetamaskRefreshRequestDtoProxy>(),
                    It.IsAny<TaskCompletionSource<MetamaskRefreshTokenResponse>>()))
                .Callback<MetamaskRefreshRequestDtoProxy, TaskCompletionSource<MetamaskRefreshTokenResponse>>(
                    (_, tcs) => tcs.SetResult(usedRefreshResponse));

            var metamaskBackendCredentialProvider = new MetamaskBackendCredentialProvider(
                walletFacadeMock, 
                signerMock.Object,
                metamaskRestRequestsAdapterMock.Object,
                jwtConverterMock.Object);

            await metamaskBackendCredentialProvider.GetCredentialAsync();
            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(expectedMetamaskCredentials.AccessToken.GetToken(), result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldNotRefreshCredentials_IfThereExistsSavedValidAccessToken()
        {
            var usedResponse = new MetamaskAccessTokenResponse();

            var walletFacadeMock = new WalletFacadeMock();
            var signerMock = new Mock<IMetamaskSignerFacade>();
            signerMock.Setup(a => a.Sign(It.IsAny<string>())).ReturnsAsync("");

            var expectedMetamaskCredentials = new JwtStore(
                new JwtToken(_validDeterminator, Int32.MaxValue, DateTime.Now),
                new JwtToken("refresh", Int32.MaxValue, DateTime.Now)
            );

            var jwtConverterMock = new Mock<IMetamaskJwtConverter>();
            jwtConverterMock.Setup(a => a.FromMetamaskAuthResponse(usedResponse)).
                Returns(expectedMetamaskCredentials);
            
            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var metamaskRestRequestsAdapterMock = new Mock<IMetamaskRestRequestsAdapter>();
            metamaskRestRequestsAdapterMock.Setup(
                    a => a.GetNonce(
                        It.IsAny<MetamaskNonceRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<MetamaskNonceResponse>>()))
                .Callback<MetamaskNonceRequestDtoProxy, TaskCompletionSource<MetamaskNonceResponse>>(
                    (_, tcs) => tcs.SetResult(new MetamaskNonceResponse()));
            metamaskRestRequestsAdapterMock.Setup(
                    a => a.AuthenticateAndGetTokens(
                        It.IsAny<MetamaskAuthRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<MetamaskAccessTokenResponse>>()))
                .Callback<MetamaskAuthRequestDtoProxy, TaskCompletionSource<MetamaskAccessTokenResponse>>(
                    (_, tcs) => tcs.SetResult(usedResponse));

            var metamaskBackendCredentialProvider = new MetamaskBackendCredentialProvider(
                walletFacadeMock, 
                signerMock.Object,
                metamaskRestRequestsAdapterMock.Object,
                jwtConverterMock.Object);

            await metamaskBackendCredentialProvider.GetCredentialAsync();
            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(expectedMetamaskCredentials.AccessToken.GetToken(), result);
        }
    }
}