using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class OccupiedPositionsCalculator : IOccupiedPositionsCalculator
    {
        private readonly ISimpleArmyStateCalculator _armyStateCalculator;

        public OccupiedPositionsCalculator(ISimpleArmyStateCalculator armyStateCalculator)
        {
            _armyStateCalculator = armyStateCalculator;
        }
        
        public List<Vector2Int> GetOccupiedPositionsAfterMove(Player forPlayer, AbstractMove afterMove)
        {
            var armyState = _armyStateCalculator.GetArmyStateAfterMove(forPlayer, afterMove);
            return GetOccupiedPositions(armyState);
        }

        public List<Vector2Int> GetOccupiedPositions(Player forPlayer)
        {
            var armyState = _armyStateCalculator.GetArmyState(forPlayer);
            return GetOccupiedPositions(armyState);
        }
        
        private List<Vector2Int> GetOccupiedPositions(SimpleCellState[,] armyState)
        {
            var occupiedPositions = new List<Vector2Int>();
            for (var i = 0; i < armyState.GetLength(0); i++)
            {
                for (var j = 0; j < armyState.GetLength(1); j++)
                {
                    if (armyState[i, j] != SimpleCellState.Neither)
                    {
                        occupiedPositions.Add(new Vector2Int(i, j));
                    }
                }
            }

            return occupiedPositions;
        }
    }
}