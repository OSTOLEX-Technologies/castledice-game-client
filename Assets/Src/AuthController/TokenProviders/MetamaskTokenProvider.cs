using System.Threading.Tasks;
using Src.AuthController.CredentialProviders.Metamask;

namespace Src.AuthController.TokenProviders
{
    public class MetamaskTokenProvider : IAccessTokenProvider
    {
        private readonly IMetamaskBackendCredentialProvider _credentialProvider;

        public MetamaskTokenProvider(IMetamaskBackendCredentialProvider credentialProvider)
        {
            _credentialProvider = credentialProvider;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var response = await _credentialProvider.GetCredentialAsync();
            return response.AccessToken;
        }
    }
}