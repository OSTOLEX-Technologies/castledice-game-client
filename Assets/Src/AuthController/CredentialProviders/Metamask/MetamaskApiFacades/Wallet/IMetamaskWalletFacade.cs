using System;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public interface IMetamaskWalletFacade
    {
        public static bool WalletConnected { get; protected set; }
        
        public void Connect();
        public string GetPublicAddress();
        public event EventHandler OnConnected;
    }
}