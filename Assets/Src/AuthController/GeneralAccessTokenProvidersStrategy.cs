using Src.AuthController.Exceptions;

namespace Src.AuthController
{
    public class GeneralAccessTokenProvidersStrategy : IAccessTokenProvidersStrategy
    {
        private readonly IFirebaseTokenProvidersFactory _firebaseFactory;
        private readonly IMetamaskTokenProvidersFactory _metamaskFactory;
        
        public GeneralAccessTokenProvidersStrategy(
            IFirebaseTokenProvidersFactory firebaseFactory,
            IMetamaskTokenProvidersFactory metamaskFactory)
        {
            _firebaseFactory = firebaseFactory;
            _metamaskFactory = metamaskFactory;
        }

        public IAccessTokenProvider GetAccessTokenProvider(AuthType authType)
        {
            switch (authType)
            {
                case AuthType.Google: return _firebaseFactory.GetTokenProvider(FirebaseAuthProviderType.Google);
                case AuthType.Metamask: return _metamaskFactory.GetTokenProvider();
                default: throw new NotSelectedAccessTokenProviderException(authType);
            }
        }
    }
}