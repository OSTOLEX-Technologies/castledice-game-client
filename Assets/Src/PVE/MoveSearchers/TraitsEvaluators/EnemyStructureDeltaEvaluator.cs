using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class EnemyStructureDeltaEvaluator : IMoveTraitEvaluator
    {
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly IUnitsStructureCalculator _unitsStructureCalculator;

        public EnemyStructureDeltaEvaluator(IBoardCellsStateCalculator boardCellsStateCalculator, IUnitsStructureCalculator unitsStructureCalculator)
        {
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _unitsStructureCalculator = unitsStructureCalculator;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var boardStateBeforeMove = _boardCellsStateCalculator.GetCurrentBoardState(move.Player);
            var structureBeforeMove = _unitsStructureCalculator.CalculateEnemyStructure(boardStateBeforeMove);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            var structureAfterMove = _unitsStructureCalculator.CalculateEnemyStructure(boardStateAfterMove);
            if (structureBeforeMove == 0)
            {
                return 0;
            }
            return (structureBeforeMove - structureAfterMove)/structureBeforeMove;
        }
    }
}