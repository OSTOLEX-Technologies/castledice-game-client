using System;
using MetaMask;
using MetaMask.Unity;
using Object = UnityEngine.Object;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public class MetamaskWalletFacade : IMetamaskWalletFacade
    {
        private MetaMaskWallet _wallet;

        public void Connect()
        {
            MetaMaskUnity.Instance.Initialize();
            MetaMaskUnity.Instance.EndSession();
            _wallet = MetaMaskUnity.Instance.Wallet;

            if (_wallet.IsConnected)
            {
                OnWalletConnected(this, EventArgs.Empty);
                return;
            }
            
            _wallet.WalletConnectedHandler += OnWalletConnected;
            _wallet.WalletAuthorizedHandler += OnWalletAuthorized;
            _wallet.Connect();
        }

        public void Disconnect()
        {
            if (_wallet is null || !_wallet.IsConnected)
            {
                Disconnected?.Invoke();
                return;
            }
            
            _wallet.WalletDisconnectedHandler += OnWalletDisconnected;
            _wallet.Dispose();
        }
        
        public string GetPublicAddress()
        {
            return MetaMaskUnity.Instance.Wallet.ConnectedAddress;
        }
        
        private void OnWalletConnected(object sender, EventArgs args)
        {
            _wallet.WalletConnectedHandler -= OnWalletConnected;
            
            IMetamaskWalletFacade.WalletConnected = true;
            Connected?.Invoke();
        }

        private void OnWalletDisconnected(object sender, EventArgs args)
        {
            _wallet.WalletDisconnectedHandler -= OnWalletDisconnected;
            
            IMetamaskWalletFacade.WalletConnected = false;
            
            MetaMaskUnity.Instance.EndSession();
            
            var metamaskUnityComponentGameObject = MetaMaskUnity.Instance.gameObject;
            Object.Destroy(metamaskUnityComponentGameObject);
            GC.Collect();

            Disconnected?.Invoke();
        }
        
        private void OnWalletAuthorized(object sender, EventArgs args)
        {
            _wallet.WalletAuthorizedHandler -= OnWalletAuthorized;
            
            IMetamaskWalletFacade.WalletAuthorized = true;
            Authorized?.Invoke();
        }

        public event Action Connected;
        public event Action Disconnected;
        public event Action Authorized;
    }
}