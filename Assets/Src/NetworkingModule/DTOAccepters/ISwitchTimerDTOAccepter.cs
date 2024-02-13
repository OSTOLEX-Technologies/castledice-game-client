using castledice_events_logic.ServerToClient;

namespace Src.NetworkingModule.DTOAccepters
{
    public interface ISwitchTimerDTOAccepter
    {
        void AcceptSwitchTimerDTO(SwitchTimerDTO dto);
    }
}