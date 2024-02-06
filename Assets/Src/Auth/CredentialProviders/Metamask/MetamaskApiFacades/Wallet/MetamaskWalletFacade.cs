using System;
using MetaMask.Runtime;
using MetaMask.Scripts;
using Object = UnityEngine.Object;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public class MetamaskWalletFacade : IMetamaskWalletFacade
    {
        private MetaMaskWallet _wallet;

        public void Connect()
        {
            MetaMaskUnity.Instance.Initialize();
            _wallet = MetaMaskUnity.Instance.Wallet;

            if (_wallet.IsConnected)
            {
                OnWalletConnected(this, EventArgs.Empty);
                return;
            }
            
            _wallet.WalletConnectedHandler += OnWalletConnected;
            _wallet.Connect();
        }

        public void Disconnect()
        {
            if (_wallet is null || !_wallet.IsConnected)
            {
                OnDisconnected?.Invoke(this, EventArgs.Empty);
                return;
            }
            
            _wallet.WalletDisconnectedHandler += OnWalletDisconnected;
            _wallet.Dispose();
        }
        
        public string GetPublicAddress()
        {
            return MetaMaskUnity.Instance.Wallet?.SelectedAddress;
        }
        
        private void OnWalletConnected(object sender, EventArgs args)
        {
            _wallet.WalletConnectedHandler -= OnWalletConnected;
            
            IMetamaskWalletFacade.WalletConnected = true;
            OnConnected?.Invoke(sender, args);
        }

        private void OnWalletDisconnected(object sender, EventArgs args)
        {
            _wallet.WalletDisconnectedHandler -= OnWalletDisconnected;
            
            IMetamaskWalletFacade.WalletConnected = false;
            
            MetaMaskUnity.Instance.EndSession();
            
            var metamaskUnityComponentGameObject = MetaMaskUnity.Instance.gameObject;
            Object.Destroy(metamaskUnityComponentGameObject);
            GC.Collect();

            OnDisconnected?.Invoke(sender, args);
        }

        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
    }
}