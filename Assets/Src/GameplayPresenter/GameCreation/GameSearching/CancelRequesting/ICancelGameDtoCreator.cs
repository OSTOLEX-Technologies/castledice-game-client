using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;

namespace Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting
{
    public interface ICancelGameDtoCreator
    {
        public Task<CancelGameDTO> CreateDtoAsync();
    }
}