using Src.PVE.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public interface IUnitsStructureCalculator
    {
        public float CalculateFriendlyStructure(CellState[,] boardState);
        public float CalculateEnemyStructure(CellState[,] boardState);
    }
}