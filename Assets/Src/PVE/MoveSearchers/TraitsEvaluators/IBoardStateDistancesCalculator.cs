using Src.PVE.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public interface IBoardStateDistancesCalculator
    {
        float GetMinimalDistanceBetweenCellStates(CellState[,] boardState, CellState firstCellState, CellState secondCellState);
    }
}