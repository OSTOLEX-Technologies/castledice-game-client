using casltedice_events_logic.ServerToClient;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule
{
    public class GiveActionPointsAccepter : IGiveActionPointsDTOAccepter
    {
        private readonly IActionPointsGivingPresenter _presenter;

        public GiveActionPointsAccepter(IActionPointsGivingPresenter presenter)
        {
            _presenter = presenter;
        }

        public void AcceptGiveActionPointsDTO(GiveActionPointsDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}