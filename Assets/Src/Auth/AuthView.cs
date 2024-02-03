﻿using System;
using System.Threading.Tasks;
using MetaMask.Scripts.Transports.Unity.UGUI;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.TokenProviders;
using Src.Caching;
using Src.Components;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Src.Auth
{
    public class AuthView : MonoBehaviour, IAuthView
    {
        [SerializeField, InspectorName("SceneLoader component")]
        private SceneLoader sceneLoader;

        [SerializeField, InspectorName("Main menu Scene")]
        private SceneAsset mainMenuScene;
        
        [SerializeField, InspectorName("Metamask Auth Cancellation Canvas")]
        private Canvas metamaskAuthCancellationCanvas;
        
        [SerializeField, InspectorName("Sign in Message Canvas")]
        private Canvas signInMessageCanvas;
        [SerializeField, InspectorName("Sign in Message Text Field")]
        private TextMeshProUGUI signInMessageText;

        private AuthController _authController;
        private IObjectCacher _singletonCacher;
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

        
        #region Init
        public void Init(IObjectCacher cacher, IMetamaskWalletFacade metamaskWalletFacade, AuthController controller)
        {
            _singletonCacher = cacher;
            _metamaskWalletFacade = metamaskWalletFacade;
            _authController = controller;

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
            sceneLoader.LoadSceneWithTransition(mainMenuScene.name);
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