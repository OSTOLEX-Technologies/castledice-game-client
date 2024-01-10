using System.Threading.Tasks;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.AuthController.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.AuthController.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.AuthController.JwtManagement;
using Src.AuthController.JwtManagement.Converters.Metamask;
using Src.AuthController.REST.REST_Request_Proxies.Metamask;
using Src.AuthController.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.AuthController.CredentialProviders.Metamask
{
    public class MetamaskBackendCredentialProvider : IMetamaskBackendCredentialProvider
    {
        private bool TokenIsStored => _tokenStore != null;
        
        private readonly IMetamaskWalletFacade _walletFacade;
        private readonly IMetamaskSignerFacade _signerFacade;
        private readonly IMetamaskRestRequestsAdapter _metamaskRestRequestsAdapter;
        private readonly IMetamaskJwtConverter _jwtConverter;
        private JwtStore _tokenStore;

        public MetamaskBackendCredentialProvider(
            IMetamaskWalletFacade walletFacade, 
            IMetamaskSignerFacade signerFacade, 
            IMetamaskRestRequestsAdapter metamaskRestRequestsAdapter,
            IMetamaskJwtConverter jwtConverter)
        {
            _walletFacade = walletFacade;
            _signerFacade = signerFacade;
            _metamaskRestRequestsAdapter = metamaskRestRequestsAdapter;
            _jwtConverter = jwtConverter;
        }

        public async Task<string> GetCredentialAsync()
        {
            if (!TokenIsStored)
            {
                if (!IMetamaskWalletFacade.WalletConnected)
                {
                    await WaitForWalletConnect();
                }

                var signedNonce = await ObtainAndSignNonce();
                var accessResponse = await Auth(signedNonce);
                
                _tokenStore = _jwtConverter.FromMetamaskAuthResponse(accessResponse);
                
                return _tokenStore.AccessToken.GetToken();
            }
            
            if (!_tokenStore.AccessToken.Valid)
            {
                var refreshResponse = await RefreshTokens();
                _tokenStore = _jwtConverter.FromMetamaskRefreshResponse(refreshResponse);

                return _tokenStore.AccessToken.GetToken();
            }
            
            return _tokenStore.AccessToken.GetToken();
        }

        private async Task WaitForWalletConnect()
        {
            var tcs = new TaskCompletionSource<object>();
            _walletFacade.OnConnected += (_, _) => tcs.SetResult(new object());
            _walletFacade.Connect();
            await tcs.Task;
        }

        private async Task<string> ObtainAndSignNonce()
        {
            var nonceTcs = new TaskCompletionSource<MetamaskNonceResponse>();

            _metamaskRestRequestsAdapter.GetNonce(
                new MetamaskNonceRequestDtoProxy(_walletFacade.GetPublicAddress()),
                nonceTcs);
            var nonce = await nonceTcs.Task;
            
            return await _signerFacade.Sign(nonce.Nonce);
        }

        private async Task<MetamaskAccessTokenResponse> Auth(string signedNonce)
        {
            var authTcs = new TaskCompletionSource<MetamaskAccessTokenResponse>();
            _metamaskRestRequestsAdapter.AuthenticateAndGetTokens(
                new MetamaskAuthRequestDtoProxy(
                    _walletFacade.GetPublicAddress(),
                    signedNonce),
                authTcs);
            var accessResponse = await authTcs.Task;

            return accessResponse;
        }
        
        private async Task<MetamaskRefreshTokenResponse> RefreshTokens()
        {
            var authTcs = new TaskCompletionSource<MetamaskRefreshTokenResponse>();
            _metamaskRestRequestsAdapter.RefreshAccessTokens(
                new MetamaskRefreshRequestDtoProxy(_tokenStore.RefreshToken.GetToken()),
                authTcs);
            var refreshResponse = await authTcs.Task;

            return refreshResponse;
        }
    }
}