using System;
using MetaMask;
using MetaMask.Unity;
using UnityEngine;
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
            
            _wallet.WalletConnected += OnWalletConnected;
            _wallet.WalletAuthorized += OnWalletAuthorized;
            _wallet.Connect();
        }

        public void Disconnect()
        {
            if (_wallet is null || !_wallet.IsConnected)
            {
                Disconnected?.Invoke();
                return;
            }
            
            _wallet.WalletDisconnected += OnWalletDisconnected;
            _wallet.Dispose();
        }
        
        public string GetPublicAddress()
        {
            return MetaMaskUnity.Instance.Wallet.ConnectedAddress;
        }
        
        private void OnWalletConnected(object sender, EventArgs args)
        {
            _wallet.WalletConnected -= OnWalletConnected;
            
            IMetamaskWalletFacade.WalletConnected = true;
            Connected?.Invoke();
        }

        private void OnWalletDisconnected(object sender, EventArgs args)
        {
            _wallet.WalletDisconnected -= OnWalletDisconnected;
            
            IMetamaskWalletFacade.WalletConnected = false;
            
            MetaMaskUnity.Instance.EndSession();
            
            var metamaskUnityComponentGameObject = ((MonoBehaviour)MetaMaskUnity.Instance).gameObject;
            Object.Destroy(metamaskUnityComponentGameObject);
            GC.Collect();

            Disconnected?.Invoke();
        }
        
        private void OnWalletAuthorized(object sender, EventArgs args)
        {
            _wallet.WalletAuthorized -= OnWalletAuthorized;
            
            IMetamaskWalletFacade.WalletAuthorized = true;
            Authorized?.Invoke();
        }

        public event Action Connected;
        public event Action Disconnected;
        public event Action Authorized;
    }
}