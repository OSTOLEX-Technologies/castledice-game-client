using System;
using Src.Auth;
using Src.Auth.CredentialProviders.Firebase;
using Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.Auth.CredentialProviders.Metamask;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter.BackendUrlProvider;
using Src.Auth.JwtManagement.Converters.Metamask;
using Src.Auth.REST;
using Src.Auth.TokenProviders.TokenProvidersFactory;
using Src.Caching;
using Src.Components;
using Src.LoadingScenes;
using Src.SceneTransitionCommands;
using UnityEngine;

namespace Src.ScenesInitializers
{
    public class AuthSceneInitializer : MonoBehaviour
    {
        [SerializeField, InspectorName("Auth View")]
        private AuthView authView;

        [SerializeField, InspectorName("Scene Loader")]
        private SceneLoader sceneLoader;
        
        [SerializeField, InspectorName("Text asset Resource Loader")]
        private TextAssetResourceLoader textAssetResourceLoader;
        
        private IObjectCacher _singletonCacher;
        private IMetamaskWalletFacade _metamaskWalletFacade;
        
        private AuthController _authController;
        private AuthSceneTransitionHandler _transitionHandler;
        
        private void Awake()
        {
            _singletonCacher = new SingletonCacher();
            _metamaskWalletFacade = new MetamaskWalletFacade();
            
            _authController = new AuthController(
                new GeneralAccessTokenProvidersStrategy(
                    new FirebaseTokenProvidersCreator(
                        new FirebaseCredentialProvider(
                            new FirebaseInternalCredentialProviderCreator(textAssetResourceLoader),
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
                authView);

            _transitionHandler = new AuthSceneTransitionHandler(sceneLoader, SceneType.MainMenu);
            authView.AuthCompleted += UnsubscribeFromAuthCompleted;
            authView.AuthCompleted += _transitionHandler.HandleTransitionCommand;
            
            authView.Init(_metamaskWalletFacade, _authController);
        }

        private void UnsubscribeFromAuthCompleted(object sender, EventArgs args)
        {
            authView.AuthCompleted -= _transitionHandler.HandleTransitionCommand;
        }
    }
}