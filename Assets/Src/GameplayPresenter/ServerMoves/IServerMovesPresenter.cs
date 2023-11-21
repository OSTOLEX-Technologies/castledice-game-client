using castledice_game_data_logic.Moves;

namespace Src.GameplayPresenter.ServerMoves
{
    public interface IServerMovesPresenter
    {
        void MakeMove(MoveData moveData);
    }
}