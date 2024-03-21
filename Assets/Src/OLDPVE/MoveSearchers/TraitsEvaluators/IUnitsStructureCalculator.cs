namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    public interface IUnitsStructureCalculator
    {
        public float CalculateFriendlyStructure(CellState[,] boardState);
        public float CalculateEnemyStructure(CellState[,] boardState);
    }
}