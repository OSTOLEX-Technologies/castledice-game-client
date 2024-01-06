using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController.CredentialProviders.Firebase.Google;
using Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.AuthController.JwtManagement;
using Src.AuthController.JwtManagement.Converters.Google;
using Src.AuthController.JwtManagement.Tokens;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.REST_Request_Proxies.Firebase.Google;
using Src.AuthController.REST.REST_Response_DTOs.Firebase.Google;
using Src.AuthController.UrlOpening;

namespace Tests.EditMode.AuthTests.CredentialProviders
{
    public class GoogleCredentialProviderTests
    {
        [Test]
        public async Task GetCredentialAsync_ShouldCreateCredentials_IfThereIsNoSavedAccessToken()
        {
            var expectedGoogleCredentials = new GoogleJwtStore(
                new JwtToken("id", Int32.MaxValue, DateTime.Now),
                new JwtToken("access", Int32.MaxValue, DateTime.Now),
                new JwtToken("refresh", Int32.MaxValue, DateTime.Now)
            );
            var googleIdResponseStub = new GoogleIdTokenResponse()
            {
                AccessToken = expectedGoogleCredentials.AccessToken.GetToken(),
                ExpiresInSeconds = Int32.MaxValue
            };

            var jwtConverterMock = new Mock<IGoogleJwtConverter>();
            jwtConverterMock.Setup(a => a.FromGoogleAuthResponse(googleIdResponseStub)).
                Returns(expectedGoogleCredentials);
            
            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var googleRestRequestsAdapterMock = new Mock<IGoogleRestRequestsAdapter>();
            googleRestRequestsAdapterMock.Setup(
                    a => a.ExchangeAuthCodeWithIdToken(
                        It.IsAny<GoogleIdTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleIdTokenResponse>>()))
                .Callback<GoogleIdTokenRequestDtoProxy, TaskCompletionSource<GoogleIdTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(googleIdResponseStub));
            
            var oAuthUrlMock = new Mock<IUrlOpener>();
            
            //Port listener immediately invokes standard callback
            var localHttpPortListenerMock = new Mock<ILocalHttpPortListener>();
            localHttpPortListenerMock.Setup(a => a.StartListening(It.IsAny<Action<string>>()))
                .Callback<Action<string>>(s => s?.Invoke(""));

            //Test object
            var googleCredentialProvider = new GoogleCredentialProvider(
                googleRestRequestsAdapterMock.Object,
                oAuthUrlMock.Object,
                localHttpPortListenerMock.Object,
                jwtConverterMock.Object);

            var result = await googleCredentialProvider.GetCredentialAsync();
            Assert.AreEqual(expectedGoogleCredentials, result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldRefreshCredentials_IfThereExistsSavedExpiredAccessToken()
        {
            var expectedGoogleCredentials = new GoogleJwtStore(
                new JwtToken("id", Int32.MaxValue, DateTime.Now),
                new JwtToken("access", 0, DateTime.Now),
                new JwtToken("refresh", Int32.MaxValue, DateTime.Now)
            );
            var googleIdResponseStub = new GoogleIdTokenResponse()
            {
                AccessToken = expectedGoogleCredentials.AccessToken.GetToken(),
                ExpiresInSeconds = 0
            };

            var usedGoogleRefreshResponse = new GoogleRefreshTokenResponse();

            
            var jwtConverterMock = new Mock<IGoogleJwtConverter>();
            jwtConverterMock.Setup(a => a.FromGoogleAuthResponse(googleIdResponseStub)).
                Returns(expectedGoogleCredentials);
            jwtConverterMock.Setup(a => a.FromGoogleRefreshResponse(
                It.IsAny<GoogleJwtStore>(), 
                usedGoogleRefreshResponse)).
                Returns(expectedGoogleCredentials);

            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var googleRestRequestsAdapterMock = new Mock<IGoogleRestRequestsAdapter>();
            googleRestRequestsAdapterMock.Setup(
                    a => a.ExchangeAuthCodeWithIdToken(
                        It.IsAny<GoogleIdTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleIdTokenResponse>>()))
                .Callback<GoogleIdTokenRequestDtoProxy, TaskCompletionSource<GoogleIdTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(googleIdResponseStub));
            googleRestRequestsAdapterMock.Setup(
                    (a => a.RefreshAccessToken(
                        It.IsAny<GoogleRefreshTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleRefreshTokenResponse>>())))
                .Callback<GoogleRefreshTokenRequestDtoProxy, TaskCompletionSource<GoogleRefreshTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(usedGoogleRefreshResponse));
            
            var oAuthUrlMock = new Mock<IUrlOpener>();
            
            //Port listener immediately invokes standard callback
            var localHttpPortListenerMock = new Mock<ILocalHttpPortListener>();
            localHttpPortListenerMock.Setup(a => a.StartListening(It.IsAny<Action<string>>()))
                .Callback<Action<string>>(s => s?.Invoke(""));

            //Test object
            var googleCredentialProvider = new GoogleCredentialProvider(
                googleRestRequestsAdapterMock.Object,
                oAuthUrlMock.Object,
                localHttpPortListenerMock.Object,
                jwtConverterMock.Object);

            //Retrieve initial response (by calling GetAuthData()) 
            await googleCredentialProvider.GetCredentialAsync();

            //Try to refresh token
            var result = await googleCredentialProvider.GetCredentialAsync();

            Assert.AreEqual(expectedGoogleCredentials, result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldNotRefreshCredentials_IfThereExistsSavedValidAccessToken()
        {
            var expectedGoogleCredentials = new GoogleJwtStore(
                new JwtToken("id", Int32.MaxValue, DateTime.Now),
                new JwtToken("access", Int32.MaxValue, DateTime.Now),
                new JwtToken("refresh", Int32.MaxValue, DateTime.Now)
            );
            var googleIdResponseStub = new GoogleIdTokenResponse()
            {
                AccessToken = expectedGoogleCredentials.AccessToken.GetToken(),
                ExpiresInSeconds = Int32.MaxValue
            };

            
            var jwtConverterMock = new Mock<IGoogleJwtConverter>();
            jwtConverterMock.Setup(a => a.FromGoogleAuthResponse(googleIdResponseStub)).
                Returns(expectedGoogleCredentials);
            
            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var googleRestRequestsAdapterMock = new Mock<IGoogleRestRequestsAdapter>();
            googleRestRequestsAdapterMock.Setup(
                    a => a.ExchangeAuthCodeWithIdToken(
                        It.IsAny<GoogleIdTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleIdTokenResponse>>()))
                .Callback<GoogleIdTokenRequestDtoProxy, TaskCompletionSource<GoogleIdTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(googleIdResponseStub));
            
            var oAuthUrlMock = new Mock<IUrlOpener>();
            
            //Port listener immediately invokes standard callback
            var localHttpPortListenerMock = new Mock<ILocalHttpPortListener>();
            localHttpPortListenerMock.Setup(a => a.StartListening(It.IsAny<Action<string>>()))
                .Callback<Action<string>>(s => s?.Invoke(""));

            //Test object
            var googleCredentialProvider = new GoogleCredentialProvider(
                googleRestRequestsAdapterMock.Object,
                oAuthUrlMock.Object,
                localHttpPortListenerMock.Object,
                jwtConverterMock.Object);

            //Retrieve initial response (by calling GetAuthData()) 
            await googleCredentialProvider.GetCredentialAsync();
            
            //Try to get existing valid token
            var result = await googleCredentialProvider.GetCredentialAsync();

            Assert.AreEqual(expectedGoogleCredentials, result);
        }
    }
}