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
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src.AuthController
{
    public class f_bb : MonoBehaviour, IAuthView
    {
        private AuthController _authController;
        private SingletonCacher _singletonCacher;

        public void LoginWithGoogle()
        {
            Login(AuthType.Google);
        }
        
        public void LoginWithMetamask()
        {
            SceneManager.LoadScene("MetAuth");
        }

        public void Play()
        {
            _authController.TokenProviderLoaded -= OnTokenProviderLoaded;
            
            Debug.Log("LOADING SCENE... GOOGLE0");
            SceneManager.LoadScene("Transition", LoadSceneMode.Single);
        }

        public void Login(AuthType authType)
        {
            AuthTypeChosen?.Invoke(this, authType);
        }

        public class ObjectCacherMock : IObjectCacher
        {
            public void CacheObject<T>(T obj)
            {
            }
        }
        public class WalletFacadeMock : IMetamaskWalletFacade
        {
            public void Connect()
            {
                throw new NotImplementedException();
            }

            public void Disconnect()
            {
                throw new NotImplementedException();
            }

            public string GetPublicAddress()
            {
                throw new NotImplementedException();
            }

            public event EventHandler OnConnected;
            public event EventHandler OnDisconnected;
        }
        private void Awake()
        {
            var cacherMock = new ObjectCacherMock();
            var walletFacadeMock = new WalletFacadeMock();
            
            _authController = new AuthController(
                new GeneralAccessTokenProvidersStrategy(
                    new FirebaseTokenProvidersCreator(
                        new FirebaseCredentialProvider(
                            new FirebaseInternalCredentialProviderCreator(),
                            new FirebaseCredentialFormatter())), 
                    new MetamaskTokenProvidersCreator(
                        new MetamaskBackendCredentialProvider(
                            walletFacadeMock,
                            new MetamaskSignerFacade(),
                            new MetamaskRestRequestsAdapter(
                                new HttpClientRequestAdapter(),
                                new MetamaskBackendUrlProvider()),
                            new MetamaskJwtConverter()))),
                cacherMock, 
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
            Play();
        }

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}