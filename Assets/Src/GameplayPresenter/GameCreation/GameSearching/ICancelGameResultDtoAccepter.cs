using castledice_events_logic.ServerToClient;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public interface ICancelGameResultDtoAccepter
    {
        public void AcceptCancelGameResultDto(CancelGameResultDTO cancelGameResultDto);
    }
}