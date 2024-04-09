using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using Src.Auth.TokenProviders;

namespace Src.NetworkingModule.DTOCreators
{
    public class InitializePlayerDtoCreator : IInitializePlayerDtoCreator
    {
        private readonly IAccessTokenProvider _accessTokenProvider;

        public InitializePlayerDtoCreator(IAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }

        public async Task<InitializePlayerDTO> CreateAsync()
        {
            var accessToken = await _accessTokenProvider.GetAccessTokenAsync();
            return new InitializePlayerDTO(accessToken);
        }
    }
}