using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.PVE.Calculators;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators
{
    public class EnemyBaseReachCostDeltaEvaluator : IMoveTraitEvaluator
    {
        private readonly Vector2Int _enemyBasePosition;
        private readonly IReachCostCalculator _reachCostCalculator;
        private readonly Player _player;

        public EnemyBaseReachCostDeltaEvaluator(Vector2Int enemyBasePosition, IReachCostCalculator reachCostCalculator, Player player)
        {
            _enemyBasePosition = enemyBasePosition;
            _reachCostCalculator = reachCostCalculator;
            _player = player;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var reachCostBefore = _reachCostCalculator.GetMinReachCost(_player, _enemyBasePosition);
            var reachCostAfter = _reachCostCalculator.GetMinReachCostAfterMove(_player, _enemyBasePosition, move);
            return reachCostBefore - reachCostAfter;
        }
    }
}