using System;
using System.Threading.Tasks;
using Src.Auth.AuthTokenSaver;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.Auth.JwtManagement;
using Src.Auth.JwtManagement.Converters.Metamask;
using Src.Auth.REST.REST_Request_Proxies.Metamask;
using Src.Auth.REST.REST_Response_DTOs.MetamaskBackend;

namespace Src.Auth.CredentialProviders.Metamask
{
    public class MetamaskBackendCredentialProvider : IMetamaskBackendCredentialProvider
    {
        private bool TokenIsStored => _tokenStore is not null;
        
        private readonly IMetamaskWalletFacade _walletFacade;
        private readonly IMetamaskSignerFacade _signerFacade;
        private readonly IMetamaskRestRequestsAdapter _metamaskRestRequestsAdapter;
        private readonly IMetamaskJwtConverter _jwtConverter;
        private readonly IAuthTokenSaver _authTokenSaver;

        private JwtStore _tokenStore;

        public MetamaskBackendCredentialProvider(
            IMetamaskWalletFacade walletFacade, 
            IMetamaskSignerFacade signerFacade, 
            IMetamaskRestRequestsAdapter metamaskRestRequestsAdapter,
            IMetamaskJwtConverter jwtConverter,
            IAuthTokenSaver authTokenSaver)
        {
            _walletFacade = walletFacade;
            _signerFacade = signerFacade;
            _metamaskRestRequestsAdapter = metamaskRestRequestsAdapter;
            _jwtConverter = jwtConverter;
            _authTokenSaver = authTokenSaver;
            _authTokenSaver.TryGetTokenStoreByAuthType(out var tempTokenStore, AuthType.Metamask);
            _tokenStore = tempTokenStore as JwtStore;
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
                // _authTokenSaver.SaveAuthTokens(_tokenStore, AuthType.Metamask);
                // return _tokenStore.AccessToken.Token;
                
                return "metamask_access_token_stub";
            }
            
            if (!_tokenStore.accessToken.Valid)
            {
                var refreshResponse = await RefreshTokens();
                _tokenStore = _jwtConverter.FromMetamaskRefreshResponse(refreshResponse);
                _authTokenSaver.SaveAuthTokens(_tokenStore, AuthType.Metamask);
                
                return _tokenStore.accessToken.Token;
            }
            
            return _tokenStore.accessToken.Token;
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
                new MetamaskRefreshRequestDtoProxy(_tokenStore.refreshToken.Token));

            return refreshResponse;
        }
    }
}