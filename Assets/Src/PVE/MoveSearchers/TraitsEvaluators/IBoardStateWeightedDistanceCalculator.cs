using Src.PVE.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public interface IBoardStateWeightedDistanceCalculator
    {
        /// <summary>
        /// Determines the minimal distance between the given board states.
        /// Takes into account the cost of each cell on the board.
        /// </summary>
        /// <param name="cellsCosts"></param>
        /// <param name="boardState"></param>
        /// <returns></returns>
        public int GetMinimalWeightedDistanceBetweenStates(int[,] cellsCosts, CellState[,] boardState, CellState from, CellState to);
    }
}