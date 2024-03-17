using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.PVE.Calculators;

namespace Src.PVE.GameSituations
{
    /// <summary>
    /// Determines whether or not exactly all positions listed in <b>expectedPositions</b> list are occupied by <b>occupiedBy</b> player.
    /// If some positions listed in <b>expectedPositions</b> list are not occupied by <b>occupiedBy</b> player, then this situation is not satisfied.
    /// If some positions not listed in <b>expectedPositions</b> list are occupied by <b>occupiedBy</b> player, then this situation is not satisfied as well. 
    /// </summary>
    public class OccupiedPositionsSituation : IGameSituation
    {
        private readonly List<Vector2Int> _expectedPositions;
        private readonly IOccupiedPositionsCalculator _occupiedPositionsCalculator;
        private readonly Player _occupiedBy;
        
        public OccupiedPositionsSituation(List<Vector2Int> expectedPositions, IOccupiedPositionsCalculator occupiedPositionsCalculator, Player occupiedBy)
        {
            _expectedPositions = expectedPositions;
            _occupiedPositionsCalculator = occupiedPositionsCalculator;
            _occupiedBy = occupiedBy;
        }

        public bool IsSituation()
        {
            var occupiedPositions = _occupiedPositionsCalculator.GetOccupiedPositions(_occupiedBy);
            foreach (var position in occupiedPositions)
            {
                if (!_expectedPositions.Contains(position))
                {
                    return false;
                }
            }
            return occupiedPositions.Count == _expectedPositions.Count;
        }
    }
}