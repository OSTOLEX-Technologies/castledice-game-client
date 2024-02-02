using System;
using System.Threading.Tasks;
using MetaMask.Scripts.Transports.Unity.UGUI;
using Src.Auth.CredentialProviders.Firebase;
using Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.Auth.CredentialProviders.Metamask;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider;
using Src.Auth.JwtManagement.Converters.Metamask;
using Src.Auth.REST;
using Src.Auth.TokenProviders;
using Src.Auth.TokenProviders.TokenProvidersFactory;
using Src.Caching;
using Src.Components;
using TMPro;
using UnityEngine;

namespace Src.Auth
{
    public class AuthView : MonoBehaviour, IAuthView
    {
        [SerializeField, InspectorName("SceneLoader component")]
        private SceneLoader sceneLoader;
        
        [SerializeField, InspectorName("Metamask Auth Cancellation Canvas")]
        private Canvas metamaskAuthCancellationCanvas;
        
        [SerializeField, InspectorName("Sign in Message Canvas")]
        private Canvas signInMessageCanvas;
        [SerializeField, InspectorName("Sign in Message Text Field")]
        private TextMeshProUGUI signInMessageText;

        private AuthController _authController;
        private SingletonCacher _singletonCacher;
        private IMetamaskWalletFacade _metamaskWalletFacade;

        private MetaMaskUnityUIHandler _qrCodeHandlerCanvas;
        
        private Action _metamaskProviderDisconnected;

        #region PublicMethods
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
            AuthTypeChosen?.Invoke(this, authType);
        }

        public void CancelMetamaskAuth()
        {
            metamaskAuthCancellationCanvas.gameObject.SetActive(false);
            
            _qrCodeHandlerCanvas ??= FindObjectOfType<MetaMaskUnityUIHandler>();
            _qrCodeHandlerCanvas.gameObject.SetActive(false);
        }
        #endregion

        
        #region UnityMethods
        private void Awake()
        {
            _singletonCacher = new SingletonCacher();
            _metamaskWalletFacade = new MetamaskWalletFacade();
            
            _authController = new AuthController(
                new GeneralAccessTokenProvidersStrategy(
                    new FirebaseTokenProvidersCreator(
                        new FirebaseCredentialProvider(
                            new FirebaseInternalCredentialProviderCreator(),
                            new FirebaseCredentialFormatter())), 
                    new MetamaskTokenProvidersCreator(
                        new MetamaskBackendCredentialProvider(
                            _metamaskWalletFacade,
                            new MetamaskSignerFacade(),
                            new MetamaskRestRequestsAdapter(
                                new HttpClientRequestAdapter(),
                                new MetamaskBackendUrlProvider()),
                            new MetamaskJwtConverter()))),
                _singletonCacher, 
                this);
            
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
            sceneLoader.LoadTransitionScene();
        }
        #endregion
        
        
        #region AuthProcessEnding
        private async Task WaitUntilMetamaskDisconnects()
        {
            var disconnectTsc = new TaskCompletionSource<object>();
            void OnMetamaskUnityDisconnected(object sender, EventArgs args)
            {
                _metamaskWalletFacade.OnDisconnected -= OnMetamaskUnityDisconnected;
                disconnectTsc.SetResult(new object());
            }

            _metamaskWalletFacade.OnDisconnected += OnMetamaskUnityDisconnected;
            _metamaskWalletFacade.Disconnect();

            await disconnectTsc.Task;
        }
        #endregion
        

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}