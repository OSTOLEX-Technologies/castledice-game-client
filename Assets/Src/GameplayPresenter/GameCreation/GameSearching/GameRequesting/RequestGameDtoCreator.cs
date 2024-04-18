using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using Src.Auth.TokenProviders;

namespace Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting
{
    public class RequestGameDtoCreator : IRequestGameDtoCreator
    {
        private readonly IAccessTokenProvider _tokenProvider;
        
        public RequestGameDtoCreator(IAccessTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }
        
        public async Task<RequestGameDTO> CreateDtoAsync()
        {
            var token = await _tokenProvider.GetAccessTokenAsync();
            return new RequestGameDTO(token);
        }
    }
}