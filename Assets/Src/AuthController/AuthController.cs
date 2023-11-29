using System;
using IObjectCacher = Src.Caching.IObjectCacher;

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
        }

        public void AuthorizeWith(AuthType authType)
        {
            throw new NotImplementedException();
        }

        private void OnAuthTypeChosen(object sender, AuthType authType)
        {
            throw new NotImplementedException();   
        }
    }
}