using System.Threading.Tasks;
using Src.AuthController.Exceptions;

namespace Src.AuthController.TokenProviders.TokenProvidersFactory
{
    public class GeneralAccessTokenProvidersStrategy : IAccessTokenProvidersStrategy
    {
        private readonly IFirebaseTokenProvidersCreator _firebaseProviderCreator;
        private readonly IMetamaskTokenProvidersCreator _metamaskProvidersCreator;
        
        public GeneralAccessTokenProvidersStrategy(
            IFirebaseTokenProvidersCreator firebaseProviderCreator,
            IMetamaskTokenProvidersCreator metamaskProvidersCreator)
        {
            _firebaseProviderCreator = firebaseProviderCreator;
            _metamaskProvidersCreator = metamaskProvidersCreator;
        }

        public async Task<IAccessTokenProvider> GetAccessTokenProviderAsync(AuthType authType)
        {
            switch (authType)
            {
                case AuthType.Google: return await _firebaseProviderCreator.GetTokenProviderAsync(FirebaseAuthProviderType.Google);
                case AuthType.Metamask: return await _metamaskProvidersCreator.GetTokenProviderAsync();
                default: throw new AccessTokenProviderNotFoundException(authType);
            }
        }
    }
}