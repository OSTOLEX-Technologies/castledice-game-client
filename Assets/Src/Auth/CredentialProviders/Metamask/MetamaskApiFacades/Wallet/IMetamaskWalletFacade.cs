using System;
using UnityEngine.Events;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet
{
    public interface IMetamaskWalletFacade
    {
        public static bool WalletConnected { get; protected set; }
        
        public void Connect();
        public void Disconnect();
        public string GetPublicAddress();
        
        public event UnityAction OnConnected;
        public event UnityAction OnDisconnected;
    }
}