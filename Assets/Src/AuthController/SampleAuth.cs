using System;
using Src.AuthController.TokenProviders.TokenProvidersFactory;
using Src.Caching;
using UnityEngine;

namespace Src.AuthController
{
    public class SampleAuth : MonoBehaviour, IAuthView
    {
        private AuthController authController;
        private void Awake()
        {
            authController = new AuthController(
                new GeneralAccessTokenProvidersStrategy(
                    new FirebaseTokenProvidersFactory(
                        new FirebaseCredentialProvider()), 
                    new SampleMetamaskProviderStub()),
                new SingletonCacher(), 
                this);
        }

        private void Start()
        {
            AuthTypeChosen?.Invoke(this, AuthType.Google);
        }

        public void ShowSignInResult()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<AuthType> AuthTypeChosen;
    }
}