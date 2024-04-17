using castledice_events_logic.ClientToServer;

namespace Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting
{
    public interface IRequestGameDtoSender
    {
        public void SendDto(RequestGameDTO dto);
    }
}