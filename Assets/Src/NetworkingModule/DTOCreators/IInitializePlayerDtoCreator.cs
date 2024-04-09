using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;

namespace Src.NetworkingModule.DTOCreators
{
    public interface IInitializePlayerDtoCreator
    {
        Task<InitializePlayerDTO> CreateAsync();
    }
}