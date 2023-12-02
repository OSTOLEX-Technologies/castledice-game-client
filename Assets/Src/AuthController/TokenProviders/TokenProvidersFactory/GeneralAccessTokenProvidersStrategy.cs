using System.Threading.Tasks;
using Src.AuthController.Exceptions;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
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

        public async Task<IAccessTokenProvider> GetAccessTokenProviderAsync(AuthType authType)
        {
            switch (authType)
            {
                case AuthType.Google: return await _firebaseFactory.GetTokenProviderAsync(FirebaseAuthProviderType.Google);
                case AuthType.Metamask: return await _metamaskFactory.GetTokenProviderAsync();
                default: throw new NotSelectedAccessTokenProviderException(authType);
            }
        }
    }
}