using castledice_game_logic;
using castledice_game_logic.Math;
using Src.PVE.Calculators;

namespace Src.PVE.GameSituations
{
    /// <summary>
    /// Method IsSituation in this class answers the question "Is it possible for player to reach given position if he
    /// has maximum amount of action points?"
    /// </summary>
    public class PositionCanBeReachedSituation : IGameSituation
    {
        private readonly Vector2Int _position;
        private readonly IReachCostCalculator _reachCostCalculator;
        private readonly int _maxActionPoints;
        private readonly Player _reachedBy;

        public PositionCanBeReachedSituation(Vector2Int position, IReachCostCalculator reachCostCalculator, int maxActionPoints, Player reachedBy)
        {
            _position = position;
            _reachCostCalculator = reachCostCalculator;
            _maxActionPoints = maxActionPoints;
            _reachedBy = reachedBy;
        }

        public bool IsSituation()
        {
            return _reachCostCalculator.GetMinReachCost(_reachedBy, _position) <= _maxActionPoints;
        }
    }
}