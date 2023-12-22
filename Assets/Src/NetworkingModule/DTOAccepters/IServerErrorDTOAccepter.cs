using castledice_events_logic.ServerToClient;

namespace Src.NetworkingModule.DTOAccepters
{
    public interface IServerErrorDTOAccepter
    {
        void AcceptServerErrorDTO(ServerErrorDTO dto);
    }
}