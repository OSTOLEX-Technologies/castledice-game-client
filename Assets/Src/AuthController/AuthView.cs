using System;
using System.Threading;
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

namespace Src.AuthController
{
    public class AuthView : MonoBehaviour, IAuthView
    {
        private AuthController _authController;
        private SingletonCacher _singletonCacher;

        public void LoginWithGoogle()
        {
            AuthTypeChosen?.Invoke(this, AuthType.Google);
        }
        public void LoginWithMetamask()
        {
            AuthTypeChosen?.Invoke(this, AuthType.Metamask);
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


        private Thread thr;
        private void Start()
        {
            _authController.TokenProviderLoaded += OnTokenProviderLoaded;
            thr = new Thread(
                Ping);
            thr.Start();
        }

        private void OnDestroy()
        {
            thr?.Abort();
        }


        private static void Ping()
        {
            while (true)
            {
                Debug.Log("ping...");
                Thread.Sleep(500);
            }
        }

        public void ShowSignInResult()
        {
            throw new NotImplementedException();
        }
        
        private async void OnTokenProviderLoaded(object sender, EventArgs e)
        {
            var token = await Singleton<IAccessTokenProvider>.Instance.GetAccessTokenAsync();
        }

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}