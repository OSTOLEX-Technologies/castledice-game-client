using MetaMask.IO;
using MetaMask.Unity;
using UnityEngine;

namespace Src.AuthController.CredentialProviders.Metamask
{
    [RequireComponent(typeof(MetaMaskUnity))]
    [RequireComponent(typeof(MetaMaskHttpService))]
    [RequireComponent(typeof(MetaMaskUnityEventHandler))]
    public class MetamaskConnectionHandlePreserver : MonoBehaviour
    {
        private MetaMaskUnity _metaMaskUnity;
        
        private void Awake()
        {
            _metaMaskUnity = transform.GetComponent<MetaMaskUnity>();
            DontDestroyOnLoad(gameObject);
        }

        public void DisposeMetamaskServices()
        {
            // if (_metaMaskUnity.Wallet != null)
            // {
            //     _metaMaskUnity.Wallet.Dispose();
            // }
            _metaMaskUnity.Disconnect();
            //Destroy(gameObject);
        }
    }
}