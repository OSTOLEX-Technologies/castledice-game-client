using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetaMask.Transports.Unity.UI;
using Src.Analytics.Events;
using Src.Analytics.Identity;
using Src.Auth.CredentialProviders.Firebase;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.TokenProviders;
using Src.General.Caching;
using Src.General.TimeRetriever;
using TMPro;
using UnityEngine;

namespace Src.Auth
{
    public class AuthView : MonoBehaviour, IAuthView
    {
        private const string LoginTimestampParamName = "Login";
        
        [SerializeField, InspectorName("Main Auth Canvas")]
        private Canvas mainAuthCanvas;
        
        [SerializeField, InspectorName("Metamask Auth Cancellation Canvas")]
        private Canvas metamaskAuthCancellationCanvas;
        
        [SerializeField, InspectorName("Sign in Message Canvas")]
        private Canvas signInMessageCanvas;

        [SerializeField, InspectorName("Sign in Message Text Field")]
        private TextMeshProUGUI signInMessageText;

        private AuthController _authController;
        private IMetamaskWalletFacade _metamaskWalletFacade;
        private IFirebaseCredentialProvider _firebaseCredentialProvider;

        private IDateTimeRetriever _dateTimeRetriever;
        private IBranchLogin _branchLogin;

        private MetaMaskUnityUIHandler _qrCodeHandlerCanvas;
        
        private Action _metamaskProviderDisconnected;

        #region PublicMethods
        public void HideAuthUI()
        {
            mainAuthCanvas.gameObject.SetActive(false);
            metamaskAuthCancellationCanvas.gameObject.SetActive(false);
            signInMessageCanvas.gameObject.SetActive(false);
            if (_qrCodeHandlerCanvas is not null)
            {
                _qrCodeHandlerCanvas.gameObject.SetActive(false);
            }
        }
        
        public void LoginWithGoogle()
        {
            Login(AuthType.Google);
        }

        public void LoginWithMetamask()
        {
            if (_qrCodeHandlerCanvas is not null)
            {
                _qrCodeHandlerCanvas.gameObject.SetActive(true);
            }
            
            metamaskAuthCancellationCanvas.gameObject.SetActive(true);
            Login(AuthType.Metamask);
        }

        public void Login(AuthType authType)
        {
            AuthTypeChosen?.Invoke(authType);
        }

        public void CancelMetamaskAuth()
        {
            metamaskAuthCancellationCanvas.gameObject.SetActive(false);
            
            _qrCodeHandlerCanvas ??= FindObjectOfType<MetaMaskUnityUIHandler>();
            _qrCodeHandlerCanvas.gameObject.SetActive(false);
        }
        #endregion

        
        #region Init
        public void Init(
            IMetamaskWalletFacade metamaskWalletFacade, 
            AuthController controller,
            IFirebaseCredentialProvider firebaseCredentialProvider,
            IDateTimeRetriever dateTimeRetriever,
            IBranchLogin branchLogin)
        {
            _metamaskWalletFacade = metamaskWalletFacade;
            _authController = controller;
            _firebaseCredentialProvider = firebaseCredentialProvider;
            _dateTimeRetriever = dateTimeRetriever;
            _branchLogin = branchLogin;

            _authController.TokenProviderLoaded += OnTokenProviderLoaded;
        }
        #endregion


        #region ImplementedViewMethods
        public void ShowSignInMessage(string signInMessage)
        {
            signInMessageText.SetText(signInMessage);
            signInMessageCanvas.gameObject.SetActive(true);
        }

        private async void OnTokenProviderLoaded(object sender, EventArgs e)
        {
            _authController.TokenProviderLoaded -= OnTokenProviderLoaded;
            var token = await Singleton<IAccessTokenProvider>.Instance.GetAccessTokenAsync();

            await WaitUntilMetamaskDisconnects();
            
            HideAuthUI();
            
            var branchEventParams = new Dictionary<string, string>
            {
                {
                    LoginTimestampParamName, 
                    _dateTimeRetriever.GetFormattedDateTime()
                }
            };
            BranchEventSender.SendCustomEvent(
                BranchEventNames.Logout, 
                branchEventParams);
            _branchLogin.Login(token);
            
            AuthCompleted?.Invoke();
        }
        #endregion
        
        
        #region AuthProcessEnding
        private async Task WaitUntilMetamaskDisconnects()
        {
            var disconnectTsc = new TaskCompletionSource<object>();
            void OnMetamaskUnityDisconnected()
            {
                _metamaskWalletFacade.Disconnected -= OnMetamaskUnityDisconnected;
                disconnectTsc.SetResult(new object());
            }

            _metamaskWalletFacade.Disconnected += OnMetamaskUnityDisconnected;
            _metamaskWalletFacade.Disconnect();

            await disconnectTsc.Task;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                _firebaseCredentialProvider.InterruptProviderInit();
            }
        }
        #endregion
        

        public event Action<AuthType> AuthTypeChosen;
        
        public event Action AuthCompleted;
    }
}