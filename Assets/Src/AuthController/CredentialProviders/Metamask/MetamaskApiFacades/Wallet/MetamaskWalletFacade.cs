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

            wallet.WalletConnectedHandler += (_, _) =>
            {
                IMetamaskWalletFacade.WalletConnected = true;
                OnConnected?.Invoke(this, EventArgs.Empty);
            };
            wallet.Connect();
        }
        
        public string GetPublicAddress()
        {
            return MetaMaskUnity.Instance.Wallet.SelectedAddress;
        }
        
        public event EventHandler OnConnected;
    }
}