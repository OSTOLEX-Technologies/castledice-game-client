using System;
using System.Threading.Tasks;
using MetaMask;
using MetaMask.Unity;
using Object = UnityEngine.Object;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public class MetamaskWalletFacade : IMetamaskWalletFacade
    {
        private readonly MetaMaskWallet _wallet;

        public MetamaskWalletFacade()
        {
            MetaMaskUnity.Instance.Initialize();
            _wallet = MetaMaskUnity.Instance.Wallet;
        }

        public void Connect()
        {
            if (_wallet.IsConnected)
            {
                OnWalletConnected(this, EventArgs.Empty);
                return;
            }
            
            // _wallet.WalletAuthorizedHandler += OnWalletConnected;
            _wallet.WalletConnectedHandler += OnWalletConnected;
            _wallet.Connect();
        }

        public void Disconnect()
        {
            if (!_wallet.IsConnected)
            {
                OnDisconnected?.Invoke(this, EventArgs.Empty);
                return;
            }
            
            // _wallet.WalletUnauthorizedHandler += OnWalletDisconnected;
            _wallet.WalletDisconnectedHandler += OnWalletDisconnected;
            _wallet.Dispose();
        }
        
        public string GetPublicAddress()
        {
            return MetaMaskUnity.Instance.Wallet?.SelectedAddress;
        }
        
        private void OnWalletConnected(object sender, EventArgs args)
        {
            // _wallet.WalletAuthorizedHandler -= OnWalletConnected;
            _wallet.WalletConnectedHandler -= OnWalletConnected;
            
            IMetamaskWalletFacade.WalletConnected = true;
            OnConnected?.Invoke(sender, args);
        }

        private void OnWalletDisconnected(object sender, EventArgs args)
        {
            // _wallet.WalletUnauthorizedHandler -= OnWalletDisconnected;
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