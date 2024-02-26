using System;
using Src.Auth.Exceptions.Authorization;
using Src.Auth.Exceptions.HttpRequests;
using Src.Auth.TokenProviders;
using Src.Auth.TokenProviders.TokenProvidersFactory;
using Src.Caching;

namespace Src.Auth
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
            try
            {
                IAccessTokenProvider tokenProvider =
                    await _tokenProvidersStrategy.GetAccessTokenProviderAsync(authType);
                _cacher.CacheObject(tokenProvider);
                TokenProviderLoaded?.Invoke(this, EventArgs.Empty);
            }
            catch (HttpNetworkNotReachableException networkNotReachableException)
            {
                _view.ShowSignInMessage(AuthErrorMessagesConfig.NetworkError);
            }
            catch (AuthCancelledException cancelledException)
            {
                _view.ShowSignInMessage(AuthErrorMessagesConfig.CancellationError);
            }
            catch (AuthFailedException failedException)
            {
                _view.ShowSignInMessage(AuthErrorMessagesConfig.FailureError);
            }
            catch (AuthUnhandledException unhandledException)
            {
                _view.ShowSignInMessage(AuthErrorMessagesConfig.UnhandledError);
            }
        }
    }
}