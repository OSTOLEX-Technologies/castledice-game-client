namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    public interface IBoardStateDistancesCalculator
    {
        float GetMinimalDistanceBetweenCellStates(CellState[,] boardState, CellState firstCellState,
            CellState secondCellState);
    }
}