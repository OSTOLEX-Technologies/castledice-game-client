using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;

namespace Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting
{
    public interface IRequestGameDtoCreator
    {
        public Task<RequestGameDTO> CreateDtoAsync();
    }
}