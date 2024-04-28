using System.Threading.Tasks;
using Src.Auth.AuthTokenSaver;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Signer;
using Src.Auth.CredentialProviders.Metamask.MetamaskApiFacades.Wallet;
using Src.Auth.CredentialProviders.Metamask.MetamaskRestRequestsAdapter;
using Src.Auth.JwtManagement;
using Src.Auth.JwtManagement.Converters.Metamask;
using Src.Auth.JwtManagement.DtoConverters.Metamask;
using Src.Auth.REST.REST_Request_Proxies.Metamask;
using Src.Auth.REST.REST_Response_DTOs.MetamaskBackend;
using UnityEngine;

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
                    await WaitForWalletAuthorize();
                }

                var walletPublicAddress = _walletFacade.GetPublicAddress();
                var nonce = await ObtainNonce(walletPublicAddress);
                Debug.LogError(nonce);
                var signedNonce = await _signerFacade.Sign(nonce);
                Debug.LogError("signed:\n" + signedNonce);
                var accessResponse = await Auth(walletPublicAddress, signedNonce);
                _tokenStore = _jwtConverter.FromMetamaskAuthResponse(accessResponse);
                _authTokenSaver.SaveAuthTokens(_tokenStore, AuthType.Metamask);
                
                PrintTokens();
                
                return _tokenStore.accessToken.Token;
            }
            
            PrintTokens();

            if (_tokenStore.accessToken.Valid) return _tokenStore.accessToken.Token;
            
            var refreshResponse = await RefreshTokens();
            _tokenStore = _jwtConverter.FromMetamaskRefreshResponse(refreshResponse);
            _authTokenSaver.SaveAuthTokens(_tokenStore, AuthType.Metamask);
            
            PrintTokens();
            
            return _tokenStore.accessToken.Token;
        }

        private async Task WaitForWalletAuthorize()
        {
            var tcs = new TaskCompletionSource<object>();
            void OnAuthorizedCallback() => tcs.SetResult(new object());
            _walletFacade.Authorized += OnAuthorizedCallback;
            _walletFacade.Connect();
            await tcs.Task;
            _walletFacade.Authorized -= OnAuthorizedCallback;
        }

        private async Task<string> ObtainNonce(string publicAddress)
        {
            var response = await _metamaskRestRequestsAdapter.GetNonce(
                new MetamaskNonceRequestDtoProxy(publicAddress));
            return response.Nonce;
        }
        

        private async Task<MetamaskAccessTokenResponse> Auth(string publicAddress, string signedNonce)
        {
            var accessResponse = await _metamaskRestRequestsAdapter.AuthenticateAndGetTokens(
                new MetamaskAuthRequestDtoProxy(publicAddress, signedNonce));

            return accessResponse;
        }
        
        private async Task<MetamaskRefreshTokenResponse> RefreshTokens()
        {
            var refreshResponse = await _metamaskRestRequestsAdapter.RefreshAccessTokens(
                new MetamaskRefreshRequestDtoProxy(_tokenStore.refreshToken.Token));

            return refreshResponse;
        }
        
        private void PrintTokens()
        {
            Debug.Log($"TOKENS:\n " +
                      $"Access: {_tokenStore.accessToken.Token}\n " +
                      $"Refresh: {_tokenStore.refreshToken.Token}");
        }
    }
}