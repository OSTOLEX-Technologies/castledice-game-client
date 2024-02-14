using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src.PVE.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public interface IBoardStateCalculator
    {
        public CellState[,] GetCurrentBoardState(Player player);
        public CellState[,] GetBoardStateAfterPlayerMove(AbstractMove move);
    }
}