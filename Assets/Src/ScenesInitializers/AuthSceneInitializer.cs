using Src.Auth;
using Src.Auth.AuthTokenSaver;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.CredentialProviders.Firebase;
using Src.Auth.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.Scripts;
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
        [SerializeField, InspectorName("Scene Flow Script")]
        private AuthSceneFlow authSceneFlow;
        
        [SerializeField, InspectorName("Auth View")]
        private AuthView authView;

        [SerializeField, InspectorName("Scene Loader")]
        private SceneLoader sceneLoader;
        
        [SerializeField, InspectorName("Text asset Resource Loader")]
        private TextAssetResourceLoader textAssetResourceLoader;
        
        private IObjectCacher _singletonCacher;
        private IMetamaskWalletFacade _metamaskWalletFacade;
        private IAuthTokenSaver _authTokenSaver;
        private IFirebaseCredentialProvider _firebaseCredentialProvider;
        
        private AuthController _authController;
        private SceneTransitionHandler _transitionHandler;

        private void Awake()
        {
            _singletonCacher = new SingletonCacher();
            _metamaskWalletFacade = new MetamaskWalletFacade();
            _authTokenSaver = new AuthTokenSaver(
                new StringSaver());

            _firebaseCredentialProvider = new FirebaseCredentialProvider(
                new FirebaseInternalCredentialProviderCreator(
                    textAssetResourceLoader,
                    _authTokenSaver),
                new FirebaseCredentialFormatter());
            
            _authController = new AuthController(
                new GeneralAccessTokenProvidersStrategy(
                    new FirebaseTokenProvidersCreator(_firebaseCredentialProvider), 
                    new MetamaskTokenProvidersCreator(_authTokenSaver)),
                _singletonCacher, 
                authView);

            _transitionHandler = new SceneTransitionHandler(sceneLoader, SceneType.MainMenu);

            authView.Init(_metamaskWalletFacade, _authController, _firebaseCredentialProvider);
            
            authSceneFlow.StartSceneFlow(
                authView,
                _authTokenSaver,
                _transitionHandler);
        }
    }
}