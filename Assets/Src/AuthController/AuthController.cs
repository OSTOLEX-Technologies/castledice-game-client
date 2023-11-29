using Src.Caching;

namespace Src.AuthController
{
    public class AuthController
    {
        private readonly IAccessTokenProvidersStrategy _tokenProvidersStrategy;
        private readonly IObjectCacher _cacher;
        private readonly IAuthView _view;

        public AuthController(IAccessTokenProvidersStrategy tokenProvidersStrategy, IObjectCacher cacher, IAuthView view)
        {
            _tokenProvidersStrategy = tokenProvidersStrategy;
            _cacher = cacher;
            _view = view;
            _view.AuthTypeChosen += OnAuthTypeChosen;
        }

        ~AuthController()
        {
            _view.AuthTypeChosen -= OnAuthTypeChosen;
        }
        
        private async void OnAuthTypeChosen(object sender, AuthType authType)
        {
            IAccessTokenProvider tokenProvider = _tokenProvidersStrategy.GetAccessTokenProvider(authType);
            _cacher.CacheObject(tokenProvider);
        }
    }
}