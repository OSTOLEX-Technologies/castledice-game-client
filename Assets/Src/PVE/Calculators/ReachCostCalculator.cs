using System.Linq;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class ReachCostCalculator : IReachCostCalculator
    {
        private readonly IBoardCostsCalculator _costsCalculator;
        private readonly IOccupiedPositionsCalculator _occupiedPositionsCalculator;
        private readonly IMatrixPathMinCostSearcher _minCostSearcher;

        public ReachCostCalculator(IBoardCostsCalculator costsCalculator, IOccupiedPositionsCalculator occupiedPositionsCalculator, IMatrixPathMinCostSearcher minCostSearcher)
        {
            _costsCalculator = costsCalculator;
            _occupiedPositionsCalculator = occupiedPositionsCalculator;
            _minCostSearcher = minCostSearcher;
        }

        public int GetMinReachCost(Player forPlayer, Vector2Int toPosition)
        {
            var occupiedPositions = _occupiedPositionsCalculator.GetOccupiedPositions(forPlayer);
            if (occupiedPositions.Count == 0) return int.MaxValue;
            var costsMatrix = _costsCalculator.GetCosts(forPlayer);
            var costs = new int[occupiedPositions.Count];
            for (var i = 0; i < occupiedPositions.Count; i++)
            {
                costs[i] = _minCostSearcher.GetMinCost(costsMatrix, occupiedPositions[i], toPosition);
            }
            return costs.Min();
        }

        public int GetMinReachCostAfterMove(Player forPlayer, Vector2Int toPosition, AbstractMove afterMove)
        {
            var occupiedPositions = _occupiedPositionsCalculator.GetOccupiedPositionsAfterMove(forPlayer, afterMove);
            if (occupiedPositions.Count == 0) return int.MaxValue;
            var costsMatrix = _costsCalculator.GetCostsAfterMove(forPlayer, afterMove);
            var costs = new int[occupiedPositions.Count];
            for (var i = 0; i < occupiedPositions.Count; i++)
            {
                costs[i] = _minCostSearcher.GetMinCost(costsMatrix, occupiedPositions[i], toPosition);
            }
            return costs.Min();
        }
    }
}