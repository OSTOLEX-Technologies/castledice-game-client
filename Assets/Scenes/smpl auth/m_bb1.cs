using System;
using System.Threading.Tasks;
using Src.AuthController.CredentialProviders.Firebase;
using Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.AuthController.CredentialProviders.Metamask;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider;
using Src.AuthController.JwtManagement.Converters.Metamask;
using Src.AuthController.REST;
using Src.AuthController.TokenProviders;
using Src.AuthController.TokenProviders.TokenProvidersFactory;
using Src.Caching;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src.AuthController
{
    public class m_bb1 : MonoBehaviour, IAuthView
    {
        private AuthController _authController;
        private SingletonCacher _singletonCacher;


        private IMetamaskWalletFacade _metamaskWalletFacade;

        private Action _metamaskProviderDisconnected;

        public void MetamaskAuth()
        {
            Login(AuthType.Metamask);
        }
        public void Login(AuthType authType)
        {
            AuthTypeChosen?.Invoke(this, authType);
        }
        
        #region AuthControl
        private async void SucceedAuth()
        {
            var token = await Caching.Singleton<IAccessTokenProvider>.Instance.GetAccessTokenAsync();

            void SceneLoading()
            {
                _metamaskProviderDisconnected -= SceneLoading;
                LoadNextScene(true);
            }
            
            _metamaskProviderDisconnected += SceneLoading;
            DisconnectMetamaskProvider();
        }
        public void CancelAuth()
        {
            void SceneLoading()
            {
                _metamaskProviderDisconnected -= SceneLoading;
                LoadNextScene(false);
            }

            _metamaskProviderDisconnected += SceneLoading;
            DisconnectMetamaskProvider();
        }

        private async void LoadNextScene(bool bAuthSucceeded)
        {
            await Task.Delay(10000);
            var sceneName = bAuthSucceeded ? "Transition" : "FAuth";
            Debug.Log("LOADING SCENE... Scene: " + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        #endregion
        
        private void DisconnectMetamaskProvider()
        {
            void OnMetamaskProviderDisconnected(object sender, EventArgs args)
            {
                _metamaskWalletFacade.OnDisconnected -= OnMetamaskProviderDisconnected;
                _metamaskProviderDisconnected?.Invoke();
            }
            _metamaskWalletFacade.OnDisconnected += OnMetamaskProviderDisconnected;
            _metamaskWalletFacade.Disconnect();
        }

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
        }
        
        private void Start()
        {
            _authController.TokenProviderLoaded += OnTokenProviderLoaded;
        }

        public void ShowSignInMessage(string signInMessage)
        {
        }

        private void OnTokenProviderLoaded(object sender, EventArgs e)
        {
            _authController.TokenProviderLoaded -= OnTokenProviderLoaded;
            SucceedAuth();
        }

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}