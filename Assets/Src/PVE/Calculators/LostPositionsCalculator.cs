using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class LostPositionsCalculator : ILostPositionsCalculator
    {
        private readonly ISimpleArmyStateCalculator _armyStateCalculator;

        public LostPositionsCalculator(ISimpleArmyStateCalculator armyStateCalculator)
        {
            _armyStateCalculator = armyStateCalculator;
        }

        public List<Vector2Int> GetLostPositions(Player forPlayer, AbstractMove afterMove)
        {
            var armyStateBeforeMove = _armyStateCalculator.GetArmyState(forPlayer);
            var armyStateAfterMove = _armyStateCalculator.GetArmyStateAfterMove(forPlayer, afterMove);
            var lostPositions = new List<Vector2Int>();
            for (int i = 0; i < armyStateAfterMove.GetLength(0); i++)
            {
                for (int j = 0; j < armyStateAfterMove.GetLength(1); j++)
                {
                    var stateBefore = armyStateBeforeMove[i, j];
                    var stateAfter = armyStateAfterMove[i, j];
                    if (stateBefore != stateAfter && stateAfter == SimpleCellState.Neither)
                    {
                        lostPositions.Add(new Vector2Int(i, j));
                    }
                }                
            }
            return lostPositions;
        }
    }
}