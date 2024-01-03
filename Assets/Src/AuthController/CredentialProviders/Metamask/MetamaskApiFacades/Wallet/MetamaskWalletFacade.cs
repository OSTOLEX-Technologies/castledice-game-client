using System;
using MetaMask.Unity;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public class MetamaskWalletFacade : IMetamaskWalletFacade
    {
        public void Connect()
        {
            MetaMaskUnity.Instance.Initialize();
            
            var wallet = MetaMaskUnity.Instance.Wallet;

            wallet.WalletConnectedHandler += (_, _) => OnConnected?.Invoke(this, EventArgs.Empty);
            wallet.Connect();
        }
        
        public event EventHandler OnConnected;
    }
}