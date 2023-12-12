using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController.CredentialProviders.Firebase.Google;
using Src.AuthController.CredentialProviders.Firebase.Google.GoogleRestRequestsAdapter;
using Src.AuthController.CredentialProviders.Firebase.Google.TokenValidator;
using Src.AuthController.CredentialProviders.Firebase.Google.UrlOpening;
using Src.AuthController.REST.PortListener;
using Src.AuthController.REST.REST_Request_Proxies;
using Src.AuthController.REST.REST_Response_DTOs;
namespace Tests.EditMode.AuthTests
{
    public class GoogleCredentialProviderTests
    {
        [Test]
        public async Task GetCredentialAsync_ShouldCreateCredentials()
        {
            var expectedGoogleIdResponse = new GoogleIdTokenResponse();

            var googleAccessTokenValidatorMock = new Mock<IGoogleAccessTokenValidator>();
            
            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var googleRestRequestsAdapterMock = new Mock<IGoogleRestRequestsAdapter>();
            googleRestRequestsAdapterMock.Setup(
                    a => a.ExchangeAuthCodeWithIdToken(
                        It.IsAny<GoogleIdTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleIdTokenResponse>>()))
                .Callback<GoogleIdTokenRequestDtoProxy, TaskCompletionSource<GoogleIdTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(expectedGoogleIdResponse));
            
            var oAuthUrlMock = new Mock<IGoogleOAuthUrl>();
            
            //Port listener immediately invokes standard callback
            var localHttpPortListenerMock = new Mock<ILocalHttpPortListener>();
            localHttpPortListenerMock.Setup(a => a.StartListening(It.IsAny<Action<string>>()))
                .Callback<Action<string>>(s => s?.Invoke(""));

            //Test object
            var googleCredentialProvider = new GoogleCredentialProvider(
                googleAccessTokenValidatorMock.Object,
                googleRestRequestsAdapterMock.Object,
                oAuthUrlMock.Object,
                localHttpPortListenerMock.Object);

            var result = await googleCredentialProvider.GetCredentialAsync();
            Assert.AreSame(expectedGoogleIdResponse, result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldRefreshCredentials()
        {
            var expectedGoogleIdResponse = new GoogleIdTokenResponse();
            var usedGoogleRefreshResponse = new GoogleRefreshTokenResponse
            {
                accessToken = expectedGoogleIdResponse.accessToken,
                expiresIn = expectedGoogleIdResponse.expiresIn
            };
            
            //Validator always returns false whilst token validation
            var googleAccessTokenValidatorMock = new Mock<IGoogleAccessTokenValidator>();
            googleAccessTokenValidatorMock.Setup(a => a.ValidateAccessToken(
                It.IsAny<GoogleIdTokenResponse>(),
                It.IsAny<float>())).Returns(false);
            
            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var googleRestRequestsAdapterMock = new Mock<IGoogleRestRequestsAdapter>();
            googleRestRequestsAdapterMock.Setup(
                    a => a.ExchangeAuthCodeWithIdToken(
                        It.IsAny<GoogleIdTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleIdTokenResponse>>()))
                .Callback<GoogleIdTokenRequestDtoProxy, TaskCompletionSource<GoogleIdTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(expectedGoogleIdResponse));
            googleRestRequestsAdapterMock.Setup(
                    (a => a.RefreshAccessToken(
                        It.IsAny<GoogleRefreshTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleRefreshTokenResponse>>())))
                .Callback<GoogleRefreshTokenRequestDtoProxy, TaskCompletionSource<GoogleRefreshTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(usedGoogleRefreshResponse));
            
            var oAuthUrlMock = new Mock<IGoogleOAuthUrl>();
            
            //Port listener immediately invokes standard callback
            var localHttpPortListenerMock = new Mock<ILocalHttpPortListener>();
            localHttpPortListenerMock.Setup(a => a.StartListening(It.IsAny<Action<string>>()))
                .Callback<Action<string>>(s => s?.Invoke(""));

            //Test object
            var googleCredentialProvider = new GoogleCredentialProvider(
                googleAccessTokenValidatorMock.Object,
                googleRestRequestsAdapterMock.Object,
                oAuthUrlMock.Object,
                localHttpPortListenerMock.Object);

            //Retrieve initial response (by calling GetAuthData()) 
            await googleCredentialProvider.GetCredentialAsync();

            //Try to refresh token
            var result = await googleCredentialProvider.GetCredentialAsync();

            Assert.AreEqual(expectedGoogleIdResponse, result);
        }
        
        [Test]
        public async Task GetCredentialAsync_ShouldNotRefreshCredentials()
        {
            var expectedGoogleIdResponse = new GoogleIdTokenResponse();

            //Validator always returns true whilst token validation
            var googleAccessTokenValidatorMock = new Mock<IGoogleAccessTokenValidator>();
            googleAccessTokenValidatorMock.Setup(a => a.ValidateAccessToken(
                It.IsAny<GoogleIdTokenResponse>(),
                It.IsAny<float>())).Returns(true);
            
            //Adapter always marks passed TCS as completed and gives it expected response as a result
            var googleRestRequestsAdapterMock = new Mock<IGoogleRestRequestsAdapter>();
            googleRestRequestsAdapterMock.Setup(
                    a => a.ExchangeAuthCodeWithIdToken(
                        It.IsAny<GoogleIdTokenRequestDtoProxy>(),
                        It.IsAny<TaskCompletionSource<GoogleIdTokenResponse>>()))
                .Callback<GoogleIdTokenRequestDtoProxy, TaskCompletionSource<GoogleIdTokenResponse>>(
                    (dict, tcs) => tcs.SetResult(expectedGoogleIdResponse));
            
            var oAuthUrlMock = new Mock<IGoogleOAuthUrl>();
            
            //Port listener immediately invokes standard callback
            var localHttpPortListenerMock = new Mock<ILocalHttpPortListener>();
            localHttpPortListenerMock.Setup(a => a.StartListening(It.IsAny<Action<string>>()))
                .Callback<Action<string>>(s => s?.Invoke(""));

            //Test object
            var googleCredentialProvider = new GoogleCredentialProvider(
                googleAccessTokenValidatorMock.Object,
                googleRestRequestsAdapterMock.Object,
                oAuthUrlMock.Object,
                localHttpPortListenerMock.Object);

            //Retrieve initial response (by calling GetAuthData()) 
            await googleCredentialProvider.GetCredentialAsync();
            
            //Try to get existing valid token
            var result = await googleCredentialProvider.GetCredentialAsync();

            Assert.AreSame(expectedGoogleIdResponse, result);
        }
    }
}