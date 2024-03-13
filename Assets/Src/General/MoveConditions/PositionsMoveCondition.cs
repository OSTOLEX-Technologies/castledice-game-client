using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.General.MoveConditions
{
    public class PositionsMoveCondition : IMoveCondition
    {
        private readonly List<Vector2Int> _allowedPositions;
        
        public PositionsMoveCondition(List<Vector2Int> allowedPositions)
        {
            _allowedPositions = allowedPositions;
        }
        
        public bool IsSatisfiedBy(AbstractMove move)
        {
            return _allowedPositions.Contains(move.Position);
        }
    }
}