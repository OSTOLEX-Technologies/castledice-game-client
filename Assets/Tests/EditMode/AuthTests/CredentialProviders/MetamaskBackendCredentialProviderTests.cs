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
        [Test]
        public async Task GetCredentialAsync_ShouldCreateCredentials_IfThereIsNoSavedAccessToken()
        {
            var director = new MetamaskCredentialProviderDirector();
            var metamaskBackendCredentialProvider = director.ConstructProviderWithNoAccessToken();

            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(
                MetamaskCredentialProviderDirector.NoSavedTokensCredentials.AccessToken.Token, 
                result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldRefreshCredentials_IfThereExistsSavedExpiredAccessToken()
        {
            var director = new MetamaskCredentialProviderDirector();
            var metamaskBackendCredentialProvider = director.ConstructProviderWithInvalidAccessToken();

            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(
                MetamaskCredentialProviderDirector.ExpiredTokenCredentials.AccessToken.Token, 
                result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldNotRefreshCredentials_IfThereExistsSavedValidAccessToken()
        {
            var director = new MetamaskCredentialProviderDirector();
            var metamaskBackendCredentialProvider = director.ConstructProviderWithValidAccessToken();

            var result = await metamaskBackendCredentialProvider.GetCredentialAsync();
            Assert.AreSame(
                MetamaskCredentialProviderDirector.ValidTokenCredentials.AccessToken.Token, 
                result);
        }
        
        private class MetamaskBackendCredentialProviderBuilder
        {
            private MetamaskBackendCredentialProvider _provider;

            private readonly MetamaskAccessTokenResponse _usedResponse;
            private readonly MetamaskRefreshTokenResponse _usedRefreshResponse;
            
            private Mock<IMetamaskWalletFacade> _walletFacade;
            private Mock<IMetamaskSignerFacade> _signerFacade;
            private Mock<IMetamaskRestRequestsAdapter> _metamaskRestRequestsAdapter;
            private Mock<IMetamaskJwtConverter> _jwtConverter;

            internal MetamaskBackendCredentialProviderBuilder()
            {
                _usedResponse = new MetamaskAccessTokenResponse();
                _usedRefreshResponse = new MetamaskRefreshTokenResponse();
                
                Reset();
            }

            public void Reset()
            {
                _walletFacade = new Mock<IMetamaskWalletFacade>();
                _signerFacade = new Mock<IMetamaskSignerFacade>();
                _metamaskRestRequestsAdapter = new Mock<IMetamaskRestRequestsAdapter>();
                _jwtConverter = new Mock<IMetamaskJwtConverter>();
            }
            
            public void SetRequestWalletFacade()
            {
                _walletFacade.Setup(a => a.Connect()).Raises(
                    a => a.OnConnected += null, _walletFacade.Object, EventArgs.Empty);
            }
            public void SetRequestSignerFacade()
            {
                _signerFacade.Setup(a => a.Sign(It.IsAny<string>())).ReturnsAsync("");
            }
            public void SetRequestAdapterNonce()
            {
                _metamaskRestRequestsAdapter.Setup(
                        a => a.GetNonce(
                            It.IsAny<MetamaskNonceRequestDtoProxy>(),
                            It.IsAny<TaskCompletionSource<MetamaskNonceResponse>>()))
                    .Callback<MetamaskNonceRequestDtoProxy, TaskCompletionSource<MetamaskNonceResponse>>(
                        (_, tcs) => tcs.SetResult(new MetamaskNonceResponse()));
            }
            public void SetRequestAdapterAuth()
            {
                _metamaskRestRequestsAdapter.Setup(
                        a => a.AuthenticateAndGetTokens(
                            It.IsAny<MetamaskAuthRequestDtoProxy>(),
                            It.IsAny<TaskCompletionSource<MetamaskAccessTokenResponse>>()))
                    .Callback<MetamaskAuthRequestDtoProxy, TaskCompletionSource<MetamaskAccessTokenResponse>>(
                        (_, tcs) => tcs.SetResult(_usedResponse));
            }
            public void SetRequestAdapterRefresh()
            {
                _metamaskRestRequestsAdapter.Setup(
                        a => a.RefreshAccessTokens(
                            It.IsAny<MetamaskRefreshRequestDtoProxy>(),
                            It.IsAny<TaskCompletionSource<MetamaskRefreshTokenResponse>>()))
                    .Callback<MetamaskRefreshRequestDtoProxy, TaskCompletionSource<MetamaskRefreshTokenResponse>>(
                        (_, tcs) => tcs.SetResult(_usedRefreshResponse));
            }
            
            public void SetJwtConverterAuth(JwtStore expectedCredentials)
            {
                _jwtConverter.Setup(a => a.FromMetamaskAuthResponse(_usedResponse)).
                    Returns(expectedCredentials);
            }
            public void SetJwtConverterRefresh(JwtStore expectedCredentials)
            {
                _jwtConverter.Setup(a => a.FromMetamaskRefreshResponse(_usedRefreshResponse)).
                    Returns(expectedCredentials);
            }
            
            public MetamaskBackendCredentialProvider GetProvider()
            {
                return new MetamaskBackendCredentialProvider(
                    _walletFacade.Object,
                    _signerFacade.Object,
                    _metamaskRestRequestsAdapter.Object,
                    _jwtConverter.Object);
            }
        }

        private class MetamaskCredentialProviderDirector
        {
            public static readonly JwtStore NoSavedTokensCredentials = new(
                new JwtToken(ValidIdentifier, Int32.MaxValue, DateTime.Now), 
                new JwtToken(RefreshIdentifier, Int32.MaxValue, DateTime.Now));
            public static readonly JwtStore ExpiredTokenCredentials = new(
                new JwtToken(InvalidIdentifier, -10, DateTime.Now),
                new JwtToken(RefreshIdentifier, Int32.MaxValue, DateTime.Now));
            public static readonly JwtStore ValidTokenCredentials = new(
                new JwtToken(ValidIdentifier, Int32.MaxValue, DateTime.Now),
                new JwtToken(RefreshIdentifier, Int32.MaxValue, DateTime.Now));

            private const string ValidIdentifier = "valid";
            private const string InvalidIdentifier = "invalid";
            private const string RefreshIdentifier = "refresh";
            
            private readonly MetamaskBackendCredentialProviderBuilder _builder;

            public MetamaskCredentialProviderDirector()
            {
                _builder = new MetamaskBackendCredentialProviderBuilder();
            }

            public MetamaskBackendCredentialProvider ConstructProviderWithNoAccessToken()
            {
                _builder.Reset();
                _builder.SetRequestWalletFacade();
                _builder.SetRequestSignerFacade();
                
                _builder.SetRequestAdapterNonce();
                _builder.SetRequestAdapterAuth();
                
                _builder.SetJwtConverterAuth(NoSavedTokensCredentials);

                return _builder.GetProvider();
            }
            public MetamaskBackendCredentialProvider ConstructProviderWithInvalidAccessToken()
            {
                _builder.Reset();
                _builder.SetRequestWalletFacade();
                _builder.SetRequestSignerFacade();
                
                _builder.SetRequestAdapterNonce();
                _builder.SetRequestAdapterAuth();
                _builder.SetRequestAdapterRefresh();
                
                _builder.SetJwtConverterAuth(ExpiredTokenCredentials);
                _builder.SetJwtConverterRefresh(ExpiredTokenCredentials);

                return _builder.GetProvider();
            }
            public MetamaskBackendCredentialProvider ConstructProviderWithValidAccessToken()
            {
                _builder.Reset();
                _builder.SetRequestWalletFacade();
                _builder.SetRequestSignerFacade();
                
                _builder.SetRequestAdapterNonce();
                _builder.SetRequestAdapterAuth();
                
                _builder.SetJwtConverterAuth(ValidTokenCredentials);

                return _builder.GetProvider();
            }
        }
    }
}