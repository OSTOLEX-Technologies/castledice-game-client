using System.Threading.Tasks;
using Src.AuthController.TokenProviders;
using Src.AuthController.TokenProviders.TokenProvidersFactory;

namespace Src.AuthController
{
    public class SampleMetamaskProviderStub : IMetamaskTokenProvidersFactory
    {
        public Task<MetamaskTokenProvider> GetTokenProviderAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}