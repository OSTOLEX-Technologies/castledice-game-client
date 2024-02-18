using System.Collections.Generic;
using System.Linq;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.PVE.TraitsEvaluators;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class WeightedEnemyProximityDeltaEvaluator : IMoveTraitEvaluator
    {
        private readonly IBoardCellsCostCalculator _boardCellsCostCalculator;
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly IGraphPathMinCostSearcher _graphPathMinCostSearcher;
        private readonly Vector2Int _basePosition;

        public WeightedEnemyProximityDeltaEvaluator(IGraphPathMinCostSearcher graphPathMinCostSearcher, IBoardCellsCostCalculator boardCellsCostCalculator, IBoardCellsStateCalculator boardCellsStateCalculator, Vector2Int basePosition)
        {
            _graphPathMinCostSearcher = graphPathMinCostSearcher;
            _boardCellsCostCalculator = boardCellsCostCalculator;
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _basePosition = basePosition;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var player = move.Player;
            var costsBeforeMove = _boardCellsCostCalculator.GetInverseCellsCosts(player);
            var cellsStateBeforeMove = _boardCellsStateCalculator.GetCurrentBoardState(player);
            var enemyPositionsBeforeMove = GetEnemyPositions(cellsStateBeforeMove);
            var distancesToClosestEnemiesBeforeMove = GetDistancesToClosestEnemies(enemyPositionsBeforeMove, costsBeforeMove);
            
            var costsAfterMove = _boardCellsCostCalculator.GetInverseCellsCostsAfterMove(move);
            var cellsStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            var enemyPositionsAfterMove = GetEnemyPositions(cellsStateAfterMove);
            var distancesToClosestEnemiesAfterMove = GetDistancesToClosestEnemies(enemyPositionsAfterMove, costsAfterMove);
            
            var distanceChanged = distancesToClosestEnemiesBeforeMove.First() != distancesToClosestEnemiesAfterMove.First();
            if (distanceChanged)
            {
                //If distance changed, then it should get BIGGER
                var oldDistance = distancesToClosestEnemiesBeforeMove.First();
                var newDistance = distancesToClosestEnemiesAfterMove.First();
                return (newDistance - oldDistance)/(float)oldDistance;
            }
            else
            {
                //If distance did not change, then count of closest enemies should get SMALLER
                var oldCount = distancesToClosestEnemiesBeforeMove.Count;
                var newCount = distancesToClosestEnemiesAfterMove.Count;
                return (oldCount - newCount)/(float)oldCount;
            }
        }

        private List<int> GetDistancesToClosestEnemies(List<Vector2Int> enemyPositions, int[,] costsBeforeMove)
        {
            var distancesToEnemies = GetDistancesToEnemies(enemyPositions, costsBeforeMove);
            var minDistance = distancesToEnemies.Count > 0 ? distancesToEnemies.Min() : 0;
            return distancesToEnemies.Where(c => c == minDistance).ToList();
        }

        private List<int> GetDistancesToEnemies(List<Vector2Int> enemyPositions, int[,] costsBeforeMove)
        {
            var distancesToEnemies = new List<int>();
            foreach (var enemyPosition in enemyPositions)
            {
                var distance = _graphPathMinCostSearcher.FindMinCost(costsBeforeMove, enemyPosition, _basePosition);
                distancesToEnemies.Add(distance);
            }
            
            return distancesToEnemies;
        }


        private List<Vector2Int> GetEnemyPositions(CellState[,] cellStates)
        {
            var enemyPositions = new List<Vector2Int>();
            for (int i = 0; i < cellStates.GetLength(0); i++)
            {
                for (int j = 0; j < cellStates.GetLength(1); j++)
                {
                    var cellState = cellStates[i, j];
                    if (cellState == CellState.Enemy)
                    {
                        enemyPositions.Add(new Vector2Int(i, j));
                    }
                }
            }

            return enemyPositions;
        }
    }
}