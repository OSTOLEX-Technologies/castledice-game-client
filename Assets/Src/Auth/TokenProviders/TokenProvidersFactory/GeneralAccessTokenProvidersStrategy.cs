using System.Threading.Tasks;
using Src.Auth.Exceptions;

namespace Src.Auth.TokenProviders.TokenProvidersFactory
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
            return authType switch
            {
                AuthType.Google => await _firebaseProviderCreator.GetTokenProviderAsync(authType),
                AuthType.Metamask => await _metamaskProvidersCreator.GetTokenProviderAsync(),
                _ => throw new AccessTokenProviderNotFoundException(authType)
            };
        }
    }
}