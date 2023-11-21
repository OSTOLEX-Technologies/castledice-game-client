using casltedice_events_logic.ServerToClient;
using Src.GameplayPresenter.ServerMoves;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.Moves
{
    public class ServerMoveAccepter : IMoveFromServerDTOAccepter
    {
        private readonly IServerMovesPresenter _presenter;

        public ServerMoveAccepter(IServerMovesPresenter presenter)
        {
            _presenter = presenter;
        }

        public void AcceptMoveFromServerDTO(MoveFromServerDTO dto)
        {
            _presenter.MakeMove(dto.MoveData);
        }
    }
}