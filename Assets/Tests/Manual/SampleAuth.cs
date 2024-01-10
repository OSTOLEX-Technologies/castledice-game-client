using System;
using Src.AuthController;
using Src.AuthController.CredentialProviders.Firebase;
using Src.AuthController.CredentialProviders.Firebase.Google.CredentialFormatter;
using Src.AuthController.TokenProviders;
using Src.AuthController.TokenProviders.TokenProvidersFactory;
using Src.Caching;
using Tests.EditMode.AuthTests;
using UnityEngine;

namespace Tests.Manual
{
    public class SampleAuth : MonoBehaviour, IAuthView
    {
        private AuthController _authController;
        private SingletonCacher _singletonCacher;

        private void Awake()
        {
            _singletonCacher = new SingletonCacher();
            _authController = new AuthController(
                new GeneralAccessTokenProvidersStrategy(
                    new FirebaseTokenProvidersCreator(
                        new FirebaseCredentialProvider(
                            new FirebaseInternalCredentialProviderCreator(),
                            new FirebaseCredentialFormatter())), 
                    new SampleMetamaskProviderStub()),
                _singletonCacher, 
                this);
        }

        private void Start()
        {
            _authController.TokenProviderLoaded += OnTokenProviderLoaded;
            AuthTypeChosen?.Invoke(this, AuthType.Google);
        }

        public void ShowSignInResult()
        {
            throw new NotImplementedException();
        }
        
        private async void OnTokenProviderLoaded(object sender, EventArgs e)
        {
            var token = await Singleton<IAccessTokenProvider>.Instance.GetAccessTokenAsync();
            Debug.Log(token);
        }
        

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}