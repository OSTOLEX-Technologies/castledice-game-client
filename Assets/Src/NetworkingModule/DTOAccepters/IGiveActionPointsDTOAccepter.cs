using casltedice_events_logic.ServerToClient;

namespace Src.NetworkingModule.DTOAccepters
{
    public interface IGiveActionPointsDTOAccepter
    {
        void AcceptGiveActionPointsDTO(GiveActionPointsDTO dto);
    }
}