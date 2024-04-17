using castledice_events_logic.ClientToServer;

namespace Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting
{
    public interface ICancelGameDtoSender
    {
        public void SendDto(CancelGameDTO dto);
    }
}