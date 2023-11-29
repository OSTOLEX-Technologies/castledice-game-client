using IObjectCacher = Src.Caching.IObjectCacher;

namespace Src.AuthController
{
    public class AuthController
    {
        private readonly IObjectCacher _cacher;
        private readonly IAuthView _view;

        public AuthController(IObjectCacher cacher, IAuthView view)
        {
            _cacher = cacher;
            _view = view;
        }
    }
}