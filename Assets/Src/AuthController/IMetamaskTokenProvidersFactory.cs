namespace Src.AuthController
{
    public interface IMetamaskTokenProvidersFactory
    {
        public MetamaskTokenProvider GetTokenProvider();
    }
}