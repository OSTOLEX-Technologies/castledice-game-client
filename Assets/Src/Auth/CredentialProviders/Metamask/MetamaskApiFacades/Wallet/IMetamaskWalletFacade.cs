using System;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public interface IMetamaskWalletFacade
    {
        public static bool WalletConnected { get; protected set; }
        public static bool WalletAuthorized { get; protected set; }
        
        public void Connect();
        public void Disconnect();
        public string GetPublicAddress();
        
        public event Action Connected;
        public event Action Disconnected;
        public event Action Authorized;
    }
}