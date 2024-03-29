using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using Src.Auth.TokenProviders;

namespace Tests.EditMode.NetworkingModuleTests.DTOCreators
{
    public class AsyncInitializePlayerDTOCreator : IAsyncInitializePlayerDTOCreator
    {
        private readonly IAccessTokenProvider _accessTokenProvider;

        public AsyncInitializePlayerDTOCreator(IAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }

        public async Task<InitializePlayerDTO> GetDTOAsync()
        {
            var accessToken = await _accessTokenProvider.GetAccessTokenAsync();
            return new InitializePlayerDTO(accessToken);
        }
    }
}