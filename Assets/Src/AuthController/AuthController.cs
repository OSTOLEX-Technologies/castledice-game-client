using System;
using Src.AuthController.TokenProviders;
using Src.AuthController.TokenProviders.TokenProvidersFactory;
using Src.Caching;

namespace Src.AuthController
{
    public class AuthController
    {
        private readonly IAccessTokenProvidersStrategy _tokenProvidersStrategy;
        private readonly IObjectCacher _cacher;
        private readonly IAuthView _view;

        public event EventHandler TokenProviderLoaded; 

        public AuthController(IAccessTokenProvidersStrategy tokenProvidersStrategy, IObjectCacher cacher, IAuthView view)
        {
            _tokenProvidersStrategy = tokenProvidersStrategy;
            _cacher = cacher;
            _view = view;
            _view.AuthTypeChosen += OnAuthTypeChosenAsync;
        }

        ~AuthController()
        {
            _view.AuthTypeChosen -= OnAuthTypeChosenAsync;
        }
        
        private async void OnAuthTypeChosenAsync(object sender, AuthType authType)
        {
            IAccessTokenProvider tokenProvider = await _tokenProvidersStrategy.GetAccessTokenProviderAsync(authType);
            _cacher.CacheObject(tokenProvider);
            TokenProviderLoaded?.Invoke(this, EventArgs.Empty);
        }
    }
}