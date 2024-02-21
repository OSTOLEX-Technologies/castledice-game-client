using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class StructureDeltaEvaluator : IMoveTraitEvaluator
    {
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly IUnitsStructureCalculator _unitsStructureCalculator;

        public StructureDeltaEvaluator(IBoardCellsStateCalculator boardCellsStateCalculator, IUnitsStructureCalculator unitsStructureCalculator)
        {
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _unitsStructureCalculator = unitsStructureCalculator;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var boardStateBeforeMove = _boardCellsStateCalculator.GetCurrentBoardState(move.Player);
            var structureBeforeMove = _unitsStructureCalculator.CalculateFriendlyStructure(boardStateBeforeMove);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            var structureAfterMove = _unitsStructureCalculator.CalculateFriendlyStructure(boardStateAfterMove);
            return structureAfterMove - structureBeforeMove;
        }
    }
}