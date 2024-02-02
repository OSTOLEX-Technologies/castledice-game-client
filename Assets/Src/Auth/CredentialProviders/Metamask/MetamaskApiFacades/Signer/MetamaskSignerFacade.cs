using System.Threading.Tasks;
using MetaMask.Runtime.Models;
using MetaMask.Scripts;

namespace Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Signer
{
    public class MetamaskSignerFacade : IMetamaskSignerFacade
    {
        private const string PersonalSignMethodName = "personal_sign";

        public async Task<string> Sign(string message)
        {
            var wallet = MetaMaskUnity.Instance.Wallet;
            var request = new MetaMaskEthereumRequest
            {
                Method = PersonalSignMethodName,
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