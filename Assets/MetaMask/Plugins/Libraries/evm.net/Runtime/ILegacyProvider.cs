namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime
{
    public interface ILegacyProvider
    {
        long ChainId { get; }
        
        string ConnectedAddress { get; }

        object Request(string method, object[] parameters = null);
    }
}