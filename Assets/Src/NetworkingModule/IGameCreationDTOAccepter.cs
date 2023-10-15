using casltedice_events_logic.ServerToClient;

namespace Src.NetworkingModule
{
    public interface IGameCreationDTOAccepter
    {
        void AcceptCreateGameDTO(CreateGameDTO dto);
        void AcceptCancelGameResultDTO(CancelGameResultDTO dto);
    }
}