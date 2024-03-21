using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    public interface IBoardCellsStateCalculator
    {
        public CellState[,] GetCurrentBoardState(Player player);
        public CellState[,] GetBoardStateAfterPlayerMove(AbstractMove move);
    }
}