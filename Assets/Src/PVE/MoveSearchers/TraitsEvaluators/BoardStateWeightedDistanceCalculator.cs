using System.Collections.Generic;
using castledice_game_logic.Math;
using Src.PVE.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class BoardStateWeightedDistanceCalculator : IBoardStateWeightedDistanceCalculator
    {
        private readonly IGraphPathMinCostSearcher _graphPathMinCostSearcher;

        public BoardStateWeightedDistanceCalculator(IGraphPathMinCostSearcher graphPathMinCostSearcher)
        {
            _graphPathMinCostSearcher = graphPathMinCostSearcher;
        }

        public int GetMinimalWeightedDistanceBetweenStates(int[,] cellsCosts, CellState[,] boardState, CellState from, CellState to)
        {
            if (from == to)
            {
                return 0;
            }
            var fromPositions = GetPositionsForCellState(boardState, from);
            var toPositions = GetPositionsForCellState(boardState, to);
            int minimalDistance = int.MaxValue;
            foreach (var fromPosition in fromPositions)
            {
                foreach (var toPosition in toPositions)
                {
                    var distance = _graphPathMinCostSearcher.FindMinCost(cellsCosts, fromPosition, toPosition);
                    if (distance < minimalDistance)
                    {
                        minimalDistance = distance;
                    }
                }
            }
            return minimalDistance;
        }

        private List<Vector2Int> GetPositionsForCellState(CellState[,] boardState, CellState state)
        {
            var positions = new List<Vector2Int>();
            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                for (int j = 0; j < boardState.GetLength(1); j++)
                {
                    if (boardState[i, j] == state)
                    {
                        positions.Add(new Vector2Int(i, j));
                    }
                }
            }
            return positions;
        }
    }
}