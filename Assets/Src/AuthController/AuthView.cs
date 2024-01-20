using System;
using Src.AuthController.CredentialProviders.Firebase;
using Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.AuthController.CredentialProviders.Metamask;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider;
using Src.AuthController.JwtManagement.Converters.Metamask;
using Src.AuthController.REST;
using Src.AuthController.TokenProviders.TokenProvidersFactory;
using Src.Caching;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src.AuthController
{
    public class AuthView : MonoBehaviour, IAuthView
    {
        [SerializeField, InspectorName("Sign in Canvas")]
        private Canvas signInCanvas;
        
        [SerializeField, InspectorName("Sign in Message Canvas")]
        private Canvas signInMessageCanvas;
        [SerializeField, InspectorName("Sign in Message Text Field")]
        private TextMeshProUGUI signInMessageText;
        
        [SerializeField, InspectorName("Play Button Canvas")]
        private Canvas playCanvas;
        
        [SerializeField, InspectorName("Sign in Canvas")]
        private MetamaskConnectionHandlePreserver metamaskConnectionHandlePreserver;

        private AuthController _authController;
        private SingletonCacher _singletonCacher;

        public void LoginWithGoogle()
        {
            Login(AuthType.Google);
        }
        public void LoginWithMetamask()
        {
            Login(AuthType.Metamask);
        }

        public void Play()
        {
            metamaskConnectionHandlePreserver.DisposeMetamaskServices();
            SceneManager.LoadSceneAsync("MainMenu");
        }

        public void Login(AuthType authType)
        {
            AuthTypeChosen?.Invoke(this, authType);
        }
        
        private void Awake()
        {
            _singletonCacher = new SingletonCacher();
            _authController = new AuthController(
                new GeneralAccessTokenProvidersStrategy(
                    new FirebaseTokenProvidersCreator(
                        new FirebaseCredentialProvider(
                            new FirebaseInternalCredentialProviderCreator(),
                            new FirebaseCredentialFormatter())), 
                    new MetamaskTokenProvidersCreator(
                        new MetamaskBackendCredentialProvider(
                            new MetamaskWalletFacade(),
                            new MetamaskSignerFacade(),
                            new MetamaskRestRequestsAdapter(
                                new HttpClientRequestAdapter(),
                                new MetamaskBackendUrlProvider()),
                            new MetamaskJwtConverter()))),
                _singletonCacher, 
                this);
        }
        
        private void Start()
        {
            _authController.TokenProviderLoaded += OnTokenProviderLoaded;
        }

        public void ShowSignInMessage(string signInMessage)
        {
            signInMessageText.SetText(signInMessage);
            signInMessageCanvas.gameObject.SetActive(true);
        }
        
        private void OnTokenProviderLoaded(object sender, EventArgs e)
        {
            playCanvas.gameObject.SetActive(true);
            signInCanvas.gameObject.SetActive(false);
        }

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}