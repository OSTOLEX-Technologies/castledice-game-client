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
using UnityEngine;

namespace Src.Auth
{
    public class AuthBootstrap : MonoBehaviour
    {
        [SerializeField] private AuthView authView;
        
        private IObjectCacher _singletonCacher;
        private IMetamaskWalletFacade _metamaskWalletFacade;
        
        private AuthController _authController;
        
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
                authView);

            authView.Init(_singletonCacher, _metamaskWalletFacade, _authController);
        }
    }
}