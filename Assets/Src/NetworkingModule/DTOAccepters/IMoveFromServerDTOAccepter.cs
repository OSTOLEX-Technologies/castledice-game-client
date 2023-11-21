using casltedice_events_logic.ServerToClient;

namespace Src.NetworkingModule.DTOAccepters
{
    public interface IMoveFromServerDTOAccepter
    {
        void AcceptMoveFromServerDTO(MoveFromServerDTO dto);
    }
}