using System;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public interface IMetamaskWalletFacade
    {
        public void Connect();
        public event EventHandler OnConnected;
    }
}