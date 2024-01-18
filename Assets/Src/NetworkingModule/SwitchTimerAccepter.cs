using castledice_events_logic.ServerToClient;
using Src.GameplayPresenter.Timers;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule
{
    public class SwitchTimerAccepter : ISwitchTimerDTOAccepter
    {
        private readonly ITimersPresenter _timersPresenter;

        public SwitchTimerAccepter(ITimersPresenter timersPresenter)
        {
            _timersPresenter = timersPresenter;
        }

        public void AcceptSwitchTimerDTO(SwitchTimerDTO dto)
        {
            _timersPresenter.SwitchTimerForPlayer(dto.PlayerId, dto.TimeLeft, dto.Switch);
        }
    }
}