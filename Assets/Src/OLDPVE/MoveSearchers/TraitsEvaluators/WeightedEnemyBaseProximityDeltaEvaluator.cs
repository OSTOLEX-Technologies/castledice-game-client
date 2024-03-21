using System.Collections.Generic;
using System.Linq;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    public class WeightedEnemyBaseProximityDeltaEvaluator : IMoveTraitEvaluator
    {
        private readonly IBoardCellsCostCalculator _boardCellsCostCalculator;
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly Vector2Int _enemyBasePosition;
        private readonly IGraphPathMinCostSearcher _graphPathMinCostSearcher;

        public WeightedEnemyBaseProximityDeltaEvaluator(IGraphPathMinCostSearcher graphPathMinCostSearcher,
            IBoardCellsCostCalculator boardCellsCostCalculator, IBoardCellsStateCalculator boardCellsStateCalculator,
            Vector2Int enemyBasePosition)
        {
            _graphPathMinCostSearcher = graphPathMinCostSearcher;
            _boardCellsCostCalculator = boardCellsCostCalculator;
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _enemyBasePosition = enemyBasePosition;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            if (move.Position == _enemyBasePosition) return 1;
            var player = move.Player;
            var costsBeforeMove = _boardCellsCostCalculator.GetCellsCosts(player);
            var cellsStateBeforeMove = _boardCellsStateCalculator.GetCurrentBoardState(player);
            var allyPositionsBeforeMove = GetAllyPositions(cellsStateBeforeMove);
            var distancesToBaseBeforeMove = GetDistancesToEnemyBase(allyPositionsBeforeMove, costsBeforeMove);

            var costsAfterMove = _boardCellsCostCalculator.GetCellsCostsAfterMove(move);
            var cellsStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            var allyPositionsAfterMove = GetAllyPositions(cellsStateAfterMove);
            var distancesToBaseAfterMove = GetDistancesToEnemyBase(allyPositionsAfterMove, costsAfterMove);

            var oldDistance = distancesToBaseBeforeMove.First();
            var newDistance = distancesToBaseAfterMove.First();
            if (newDistance == oldDistance) return 0;
            return (oldDistance - newDistance) / (float)oldDistance;
        }

        private List<int> GetDistancesToEnemyBase(List<Vector2Int> enemyPositions, int[,] costsBeforeMove)
        {
            var distancesToEnemyBase = GetDistancesToUnits(enemyPositions, costsBeforeMove);
            var minDistance = distancesToEnemyBase.Count > 0 ? distancesToEnemyBase.Min() : 0;
            return distancesToEnemyBase.Where(c => c == minDistance).ToList();
        }

        private List<int> GetDistancesToUnits(List<Vector2Int> enemyPositions, int[,] costsBeforeMove)
        {
            var distancesToUnits = new List<int>();
            foreach (var enemyPosition in enemyPositions)
            {
                var distance =
                    _graphPathMinCostSearcher.FindMinCost(costsBeforeMove, enemyPosition, _enemyBasePosition);
                distancesToUnits.Add(distance);
            }

            return distancesToUnits;
        }


        private List<Vector2Int> GetAllyPositions(CellState[,] cellStates)
        {
            var enemyPositions = new List<Vector2Int>();
            for (var i = 0; i < cellStates.GetLength(0); i++)
            for (var j = 0; j < cellStates.GetLength(1); j++)
            {
                var cellState = cellStates[i, j];
                if (cellState == CellState.Friendly || cellState == CellState.FriendlyBase)
                    enemyPositions.Add(new Vector2Int(i, j));
            }

            return enemyPositions;
        }
    }
}