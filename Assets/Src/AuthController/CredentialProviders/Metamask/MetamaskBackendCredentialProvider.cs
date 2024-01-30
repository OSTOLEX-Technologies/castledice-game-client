using System;
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
        private bool TokenIsStored => _tokenStore is not null;
        
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
                
                // var signedNonce = await ObtainAndSignNonce();
                // var accessResponse = await Auth(signedNonce);
                //
                // _tokenStore = _jwtConverter.FromMetamaskAuthResponse(accessResponse);
                // return _tokenStore.AccessToken.Token;
                
                return "yuhgfdryuiop0987yui96t75567yugftdr";
            }
            
            if (!_tokenStore.AccessToken.Valid)
            {
                var refreshResponse = await RefreshTokens();
                _tokenStore = _jwtConverter.FromMetamaskRefreshResponse(refreshResponse);

                return _tokenStore.AccessToken.Token;
            }
            
            return _tokenStore.AccessToken.Token;
        }

        private async Task WaitForWalletConnect()
        {
            var tcs = new TaskCompletionSource<object>();
            void OnConnectedCallback(object o, EventArgs eventArgs) => tcs.SetResult(new object());
            _walletFacade.OnConnected += OnConnectedCallback;
            _walletFacade.Connect();
            await tcs.Task;
            _walletFacade.OnConnected -= OnConnectedCallback;
        }

        private async Task<string> ObtainAndSignNonce()
        {
            var nonce = await _metamaskRestRequestsAdapter.GetNonce(
                new MetamaskNonceRequestDtoProxy(_walletFacade.GetPublicAddress()));

            return await _signerFacade.Sign(nonce.Nonce);
        }

        private async Task<MetamaskAccessTokenResponse> Auth(string signedNonce)
        {
            var accessResponse = await _metamaskRestRequestsAdapter.AuthenticateAndGetTokens(
                new MetamaskAuthRequestDtoProxy(
                    _walletFacade.GetPublicAddress(),
                    signedNonce));

            return accessResponse;
        }
        
        private async Task<MetamaskRefreshTokenResponse> RefreshTokens()
        {
            var refreshResponse = await _metamaskRestRequestsAdapter.RefreshAccessTokens(
                new MetamaskRefreshRequestDtoProxy(_tokenStore.RefreshToken.Token));

            return refreshResponse;
        }
    }
}