﻿using System;
using Firebase;
using Firebase.Auth;
using MetaMask.Unity;
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
        [SerializeField, InspectorName("SceneLoader component")]
        private SceneLoader sceneLoader;
        
        [SerializeField, InspectorName("Sign in Canvas")]
        private Canvas signInCanvas;
        
        [SerializeField, InspectorName("Sign in Message Canvas")]
        private Canvas signInMessageCanvas;
        [SerializeField, InspectorName("Sign in Message Text Field")]
        private TextMeshProUGUI signInMessageText;

        private AuthController _authController;
        private SingletonCacher _singletonCacher;


        private IMetamaskWalletFacade _metamaskWalletFacade;

        public void LoginWithGoogle()
        {
            //_metamaskWalletFacade.OnDisconnected += MetamaskWalletDisconnected;
            //_metamaskWalletFacade.Disconnect();

            // if (MetaMaskUnity.Instance.Wallet.IsConnected)
            // {
            //     MetaMaskUnity.Instance.Wallet.Dispose();
            //     MetaMaskUnity.Instance.gameObject.SetActive(false);
            // }
            Login(AuthType.Google);
        }
        private void MetamaskWalletDisconnected(object sender, EventArgs args)
        {
            _metamaskWalletFacade.OnDisconnected -= MetamaskWalletDisconnected;
            MetaMaskUnity.Instance.gameObject.SetActive(false);
            
            //Login(AuthType.Google);
        }
        
        public void LoginWithMetamask()
        {
            MetaMaskUnity.Instance.gameObject.SetActive(true);
            Login(AuthType.Metamask);
        }

        public void Play()
        {
            //MetaMaskUnity.Instance.Wallet?.Dispose();
            Destroy(MetaMaskUnity.Instance.gameObject);
            
            //MetaMaskUnity.Instance.Disconnect();
            Debug.Log("LOADING SCENE...");
            SceneManager.LoadScene("Transition", LoadSceneMode.Single);
            //sceneLoader.LoadScene("Transition");
        }

        public void Login(AuthType authType)
        {
            AuthTypeChosen?.Invoke(this, authType);
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
            signInMessageText.SetText(signInMessage);
            signInMessageCanvas.gameObject.SetActive(true);
        }

        private void OnTokenProviderLoaded(object sender, EventArgs e)
        {
            Play();

            //FirebaseAuth.DefaultInstance.Dispose();
            //FirebaseApp.DefaultInstance.Dispose();
        }

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}