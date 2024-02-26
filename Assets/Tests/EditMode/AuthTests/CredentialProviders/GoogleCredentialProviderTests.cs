using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.Auth.CredentialProviders.Firebase.Google;
using Src.Auth.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.Auth.JwtManagement;
using Src.Auth.JwtManagement.Converters.Google;
using Src.Auth.JwtManagement.Tokens;
using Src.Auth.REST.PortListener;
using Src.Auth.REST.REST_Request_Proxies.Firebase.Google;
using Src.Auth.REST.REST_Response_DTOs.Firebase.Google;
using Src.Auth.UrlOpening;

namespace Tests.EditMode.AuthTests.CredentialProviders
{
    public class GoogleCredentialProviderTests
    {
        [Test]
        public async Task GetCredentialAsync_ShouldCreateCredentials_IfThereIsNoSavedAccessToken()
        {
            var director = new GoogleCredentialProviderDirector();
            var googleCredentialProvider = director.ConstructProviderWithNoAccessToken();

            var result = await googleCredentialProvider.GetCredentialAsync();
            Assert.AreSame(
                GoogleCredentialProviderDirector.NoSavedTokensCredentials, 
                result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldRefreshCredentials_IfThereExistsSavedExpiredAccessToken()
        {
            var director = new GoogleCredentialProviderDirector();
            var googleCredentialProvider = director.ConstructProviderWithInvalidAccessToken();

            var result = await googleCredentialProvider.GetCredentialAsync();
            Assert.AreSame(
                GoogleCredentialProviderDirector.ExpiredTokenCredentials, 
                result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldNotRefreshCredentials_IfThereExistsSavedValidAccessToken()
        {
            var director = new GoogleCredentialProviderDirector();
            var googleCredentialProvider = director.ConstructProviderWithValidAccessToken();

            var result = await googleCredentialProvider.GetCredentialAsync();
            Assert.AreSame(
                GoogleCredentialProviderDirector.ValidTokenCredentials, 
                result);
        }
        
        
        private class GoogleCredentialProviderBuilder
        {
            private GoogleCredentialProvider _provider;

            private Mock<IGoogleRestRequestsAdapter> _googleRestRequestsAdapter;
            private Mock<IUrlOpener> _authUrlOpener;
            private Mock<ILocalHttpPortListener> _localHttpListener;
            private Mock<IGoogleJwtConverter> _jwtConverter;

            internal GoogleCredentialProviderBuilder()
            {
                Reset();
            }

            public void Reset()
            {
                _googleRestRequestsAdapter = new Mock<IGoogleRestRequestsAdapter>();
                _authUrlOpener = new Mock<IUrlOpener>();
                _localHttpListener = new Mock<ILocalHttpPortListener>();
                _jwtConverter = new Mock<IGoogleJwtConverter>();
            }
            
            public void SetRequestAdapterAuth(GoogleIdTokenResponse responseStub)
            {
                _googleRestRequestsAdapter.Setup(
                        a => a.ExchangeAuthCodeWithIdToken(
                            It.IsAny<GoogleIdTokenRequestDtoProxy>(),
                            It.IsAny<TaskCompletionSource<GoogleIdTokenResponse>>()))
                    .Callback<GoogleIdTokenRequestDtoProxy, TaskCompletionSource<GoogleIdTokenResponse>>(
                        (dict, tcs) => tcs.SetResult(responseStub));
                
            }
            public void SetRequestAdapterRefresh(GoogleRefreshTokenResponse refreshResponseStub)
            {
                _googleRestRequestsAdapter.Setup(
                        (a => a.RefreshAccessToken(
                            It.IsAny<GoogleRefreshTokenRequestDtoProxy>())))
                    .ReturnsAsync(refreshResponseStub);
            }
            
            public void SetHttpListener()
            {
                _localHttpListener.Setup(a => a.StartListening(It.IsAny<Action<string>>()))
                    .Callback<Action<string>>(s => s?.Invoke(""));
            }
            
            public void SetJwtConverterAuth(GoogleIdTokenResponse idResponseStub, GoogleJwtStore expectedCredentials)
            {
                _jwtConverter.Setup(a => a.FromGoogleAuthResponse(idResponseStub)).
                    Returns(expectedCredentials);
            }
            public void SetJwtConverterRefresh(GoogleRefreshTokenResponse refreshResponseStub, GoogleJwtStore expectedCredentials)
            {
                
                _jwtConverter.Setup(a => a.FromGoogleRefreshResponse(
                        It.IsAny<GoogleJwtStore>(), 
                        refreshResponseStub)).
                    Returns(expectedCredentials);
            }
            
            public GoogleCredentialProvider GetProvider()
            {
                return new GoogleCredentialProvider(
                    _googleRestRequestsAdapter.Object,
                    _authUrlOpener.Object,
                    _localHttpListener.Object,
                    _jwtConverter.Object);
            }
        }

        private class GoogleCredentialProviderDirector
        {
            public static readonly GoogleJwtStore NoSavedTokensCredentials = new(
                new JwtToken(IdIdentifier, Int32.MaxValue, DateTime.Now),
                new JwtToken(AccessIdentifier, Int32.MaxValue, DateTime.Now),
                new JwtToken(RefreshIdentifier, Int32.MaxValue, DateTime.Now)
            );
            private static readonly GoogleIdTokenResponse NoSavedTokensResponseStub = new()
            {
                AccessToken = NoSavedTokensCredentials.AccessToken.Token,
                ExpiresInSeconds = Int32.MaxValue
            };

            public static readonly GoogleJwtStore ExpiredTokenCredentials = new(
                new JwtToken(IdIdentifier, Int32.MaxValue, DateTime.Now),
                new JwtToken(AccessIdentifier, 0, DateTime.Now),
                new JwtToken(RefreshIdentifier, Int32.MaxValue, DateTime.Now)
            );
            private static readonly GoogleIdTokenResponse ExpiredTokenResponseStub = new()
            {
                AccessToken = ExpiredTokenCredentials.AccessToken.Token,
                ExpiresInSeconds = 0
            };

            public static readonly GoogleJwtStore ValidTokenCredentials = new(
                new JwtToken(IdIdentifier, Int32.MaxValue, DateTime.Now),
                new JwtToken(AccessIdentifier, Int32.MaxValue, DateTime.Now),
                new JwtToken(RefreshIdentifier, Int32.MaxValue, DateTime.Now)
            );
            private static readonly GoogleIdTokenResponse ValidTokenResponseStub = new()
            {
                AccessToken = ValidTokenCredentials.AccessToken.Token,
                ExpiresInSeconds = Int32.MaxValue
            };
            
            private static readonly GoogleRefreshTokenResponse RefreshResponseStub = new();

            private const string IdIdentifier = "id";
            private const string AccessIdentifier = "access";
            private const string RefreshIdentifier = "refresh";
            
            private readonly GoogleCredentialProviderBuilder _builder;

            public GoogleCredentialProviderDirector()
            {
                _builder = new GoogleCredentialProviderBuilder();
            }

            public GoogleCredentialProvider ConstructProviderWithNoAccessToken()
            {
                _builder.Reset();
                _builder.SetHttpListener();
                
                _builder.SetRequestAdapterAuth(NoSavedTokensResponseStub);
                
                _builder.SetJwtConverterAuth(NoSavedTokensResponseStub, NoSavedTokensCredentials);

                return _builder.GetProvider();
            }
            public GoogleCredentialProvider ConstructProviderWithInvalidAccessToken()
            {
                _builder.Reset();
                _builder.SetHttpListener();
                
                _builder.SetRequestAdapterAuth(ExpiredTokenResponseStub);
                _builder.SetRequestAdapterRefresh(RefreshResponseStub);
                
                _builder.SetJwtConverterAuth(ExpiredTokenResponseStub, ExpiredTokenCredentials);
                _builder.SetJwtConverterRefresh(RefreshResponseStub, ExpiredTokenCredentials);

                return _builder.GetProvider();
            }
            public GoogleCredentialProvider ConstructProviderWithValidAccessToken()
            {
                _builder.Reset();
                _builder.SetHttpListener();
                
                _builder.SetRequestAdapterAuth(ValidTokenResponseStub);
                
                _builder.SetJwtConverterAuth(ValidTokenResponseStub, ValidTokenCredentials);

                return _builder.GetProvider();
            }
        }
    }
}