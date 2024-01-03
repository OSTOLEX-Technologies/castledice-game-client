using System.Threading.Tasks;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.CredentialProviders.Metamask
{
    public class MetamaskBackendCredentialProvider : IMetamaskBackendCredentialProvider
    {
        private readonly IMetamaskWalletFacade _walletFacade;
        private readonly IMetamaskSignerFacade _signerFacade;
        private readonly IMetamaskRestRequestsAdapter _metamaskRestRequestsAdapter;

        public MetamaskBackendCredentialProvider(IMetamaskWalletFacade walletFacade, IMetamaskSignerFacade signerFacade, IMetamaskRestRequestsAdapter metamaskRestRequestsAdapter)
        {
            _walletFacade = walletFacade;
            _signerFacade = signerFacade;
            _metamaskRestRequestsAdapter = metamaskRestRequestsAdapter;
        }

        public async Task<MetamaskAccessTokenResponse> GetCredentialAsync()
        {
            if (!IMetamaskWalletFacade.WalletConnected)
            {
                await WaitForWalletConnect();
            }

            var nonceTcs = new TaskCompletionSource<MetamaskNonceResponse>();

            _metamaskRestRequestsAdapter.GetNonce(
                new MetamaskNonceRequestDtoProxy(_walletFacade.GetPublicAddress()),
                nonceTcs);
            var nonce = await nonceTcs.Task;
            
            var signedNonce = await _signerFacade.Sign(nonce.Nonce);
            
            var authTcs = new TaskCompletionSource<MetamaskAccessTokenResponse>();
            _metamaskRestRequestsAdapter.AuthenticateAndGetTokens(
                new MetamaskAuthRequestDtoProxy(_walletFacade.GetPublicAddress(), signedNonce),
                authTcs);
            var accessToken = await authTcs.Task;

            return accessToken;
        }

        private async Task WaitForWalletConnect()
        {
            var tcs = new TaskCompletionSource<object>();
            _walletFacade.OnConnected += (_, _) => tcs.SetResult(new object());
            _walletFacade.Connect();
            await tcs.Task;
        }
    }
}