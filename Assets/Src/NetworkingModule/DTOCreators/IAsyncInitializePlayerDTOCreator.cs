using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;

namespace Tests.EditMode.NetworkingModuleTests.DTOCreators
{
    public interface IAsyncInitializePlayerDTOCreator
    {
        Task<InitializePlayerDTO> GetDTOAsync();
    }
}