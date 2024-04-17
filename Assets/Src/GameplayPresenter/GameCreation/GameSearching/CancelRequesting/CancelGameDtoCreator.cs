using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using Src.Auth.TokenProviders;

namespace Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting
{
    public class CancelGameDtoCreator : ICancelGameDtoCreator
    {
        private readonly IAccessTokenProvider _accessTokenProvider;
        
        public CancelGameDtoCreator(IAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }
        
        public async Task<CancelGameDTO> CreateDtoAsync()
        {
            var token = await _accessTokenProvider.GetAccessTokenAsync();
            return new CancelGameDTO(token);
        }
    }
}