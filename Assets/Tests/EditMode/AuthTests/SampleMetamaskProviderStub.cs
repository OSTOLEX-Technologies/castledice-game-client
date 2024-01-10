using System.Threading.Tasks;
using Src.AuthController.TokenProviders;
using Src.AuthController.TokenProviders.TokenProvidersFactory;

namespace Tests.EditMode.AuthTests
{
    public class SampleMetamaskProviderStub : IMetamaskTokenProvidersCreator
    {
        public Task<MetamaskTokenProvider> GetTokenProviderAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}