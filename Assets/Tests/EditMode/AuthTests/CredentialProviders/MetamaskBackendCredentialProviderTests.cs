using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController.CredentialProviders.Metamask;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend.Service;

namespace Tests.EditMode.AuthTests.CredentialProviders
{
    public class MetamaskBackendCredentialProviderTests
    {
        private class WalletFacadeMock : IMetamaskWalletFacade
        {
            internal static string PublicAddressStub = "address";
            
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
        private class ValidAuthResponseMock : MetamaskAccessTokenResponse
        {
            internal static string validDeterminator = "valid";
            internal class MetamaskBackendRefreshTokenMock : MetamaskBackendRefreshToken
            {
                public MetamaskBackendRefreshTokenMock()
                {
                    IssuedAt = 32000000;
                }
            }
            
            public ValidAuthResponseMock()
            {
                EncodedJwt = validDeterminator;
                ExpiresIn = 100000;
                RefreshToken = new MetamaskBackendRefreshTokenMock();
            }
        }
        private class InvalidAuthResponseMock : MetamaskAccessTokenResponse
        {
            internal static string invalidDeterminator = "invalid";
            internal class MetamaskBackendRefreshTokenInvalidMock : MetamaskBackendRefreshToken
            {
                public MetamaskBackendRefreshTokenInvalidMock()
                {
                    IssuedAt = 0;
                }
            }
            
            public InvalidAuthResponseMock()
            {
                EncodedJwt = invalidDeterminator;
                ExpiresIn = 1;
                RefreshToken = new MetamaskBackendRefreshTokenInvalidMock();
            }
        }
        
        
        [Test]
        public async Task GetCredentialAsync_ShouldCreateCredentials_IfThereIsNoSavedAccessToken()
        {
            var expectedResponse = new ValidAuthResponseMock();

            var walletFacadeMock = new WalletFacadeMock();
            var signerMock = new Mock<IMetamaskSignerFacade>();
            signerMock.Setup(a => a.Sign(It.IsAny<string>())).ReturnsAsync("");

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
                    (_, tcs) => tcs.SetResult(expectedResponse));

            var metamaskBackendCredentialProvider = new MetamaskBackendCredentialProvider(
                walletFacadeMock, 
                signerMock.Object,
                metamaskRestRequestsAdapterMock.Object);

            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(expectedResponse.EncodedJwt, result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldRefreshCredentials_IfThereExistsSavedExpiredAccessToken()
        {
            var usedResponse = new InvalidAuthResponseMock();

            var walletFacadeMock = new WalletFacadeMock();
            var signerMock = new Mock<IMetamaskSignerFacade>();
            signerMock.Setup(a => a.Sign(It.IsAny<string>())).ReturnsAsync("");

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
                metamaskRestRequestsAdapterMock.Object);

            await metamaskBackendCredentialProvider.GetCredentialAsync();
            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(InvalidAuthResponseMock.invalidDeterminator, result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldNotRefreshCredentials_IfThereExistsSavedValidAccessToken()
        {
            var usedResponse = new ValidAuthResponseMock();

            var walletFacadeMock = new WalletFacadeMock();
            var signerMock = new Mock<IMetamaskSignerFacade>();
            signerMock.Setup(a => a.Sign(It.IsAny<string>())).ReturnsAsync("");

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
                metamaskRestRequestsAdapterMock.Object);

            await metamaskBackendCredentialProvider.GetCredentialAsync();
            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(ValidAuthResponseMock.validDeterminator, result);
        }
    }
}