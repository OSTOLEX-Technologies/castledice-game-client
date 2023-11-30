using System;
using Src.AuthController;
using Src.Caching;

namespace Tests.Utils.Mocks
{
    public class AuthControllerMock : AuthController
    {
        public bool EventFired { get; private set; }
        private IAuthView _view;
        
        public AuthControllerMock(IAccessTokenProvidersStrategy tokenProvidersStrategy, IObjectCacher cacher,
            IAuthView view) : base(tokenProvidersStrategy, cacher, view)
        {
            view.AuthTypeChosen += (sender, type) => { EventFired = true; };
        }
    }
}