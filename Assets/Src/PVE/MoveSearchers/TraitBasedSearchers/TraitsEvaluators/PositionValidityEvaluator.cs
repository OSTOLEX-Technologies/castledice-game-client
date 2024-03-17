using castledice_game_logic.MovesLogic;
using Src.General;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators
{
    public class PositionValidityEvaluator : IMoveTraitEvaluator
    {
        private readonly IPositionsProvider _allowedPositionsProvider;

        public PositionValidityEvaluator(IPositionsProvider allowedPositionsProvider)
        {
            _allowedPositionsProvider = allowedPositionsProvider;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var allowedPositions = _allowedPositionsProvider.GetPositions();
            return allowedPositions.Contains(move.Position) ? 1 : 0;
        }
    }
}