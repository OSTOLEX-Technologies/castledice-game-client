using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src.PVE.Calculators;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators
{
    public class EnemyPositionsLossEvaluator : IMoveTraitEvaluator
    {
        private readonly Player _enemy;
        private readonly ILostPositionsCalculator _lostPositionsCalculator;

        public EnemyPositionsLossEvaluator(Player enemy, ILostPositionsCalculator lostPositionsCalculator)
        {
            _enemy = enemy;
            _lostPositionsCalculator = lostPositionsCalculator;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            return _lostPositionsCalculator.GetLostPositions(_enemy, move).Count;
        }
    }
}