using castledice_events_logic.ServerToClient;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public interface ICreateGameDtoAccepter
    {
        public void AcceptCreateGameDto(CreateGameDTO dto);
    }
}