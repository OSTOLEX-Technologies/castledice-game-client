using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.General.MoveConditions;

namespace Src.PVE.Calculators
{
    public class FreedPositionsCalculator : IFreedPositionsCalculator
    {
        private readonly ILostPositionsCalculator _lostPositionsCalculator;
        private readonly List<Player> _players;
        private readonly IMoveCondition _cellFreeingCondition;

        public FreedPositionsCalculator(ILostPositionsCalculator lostPositionsCalculator, List<Player> players, IMoveCondition cellFreeingCondition)
        {
            _lostPositionsCalculator = lostPositionsCalculator;
            _players = players;
            _cellFreeingCondition = cellFreeingCondition;
        }

        public List<Vector2Int> GetFreedPositions(AbstractMove afterMove)
        {
            var resultSet = new HashSet<Vector2Int>();
            foreach (var player in _players)
            {
                var lostPositions = _lostPositionsCalculator.GetLostPositions(player, afterMove);
                foreach (var lostPosition in lostPositions)
                {
                    resultSet.Add(lostPosition);
                }
            }

            if (_cellFreeingCondition.IsSatisfiedBy(afterMove))
            {
                resultSet.Add(afterMove.Position);
            }
            else
            {
                resultSet.Remove(afterMove.Position);
            }
            
            return new List<Vector2Int>(resultSet);
        }
    }
}