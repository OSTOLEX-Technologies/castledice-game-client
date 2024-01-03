using System.Threading.Tasks;
using MetaMask.Models;
using MetaMask.Unity;

namespace Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Signer
{
    public class MetamaskSignerFacade : IMetamaskSignerFacade
    {
        public async Task<string> Sign(string message)
        {
            var wallet = MetaMaskUnity.Instance.Wallet;
            var request = new MetaMaskEthereumRequest
            {
                Method = "personal_sign",
                Parameters = new object[]
                {
                    message,
                    MetaMaskUnity.Instance.Wallet.SelectedAddress,
                }
            };
            return await wallet.Request(request) as string;
        }
    }
}