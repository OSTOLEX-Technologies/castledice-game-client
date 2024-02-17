using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.PVE.TraitsEvaluators;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class WeightedEnemyProximityDeltaEvaluator : IMoveTraitEvaluator
    {
        private readonly Board _board;
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly IGraphPathMinCostSearcher _graphPathMinCostSearcher;

        public WeightedEnemyProximityDeltaEvaluator(Board board, IBoardCellsStateCalculator boardCellsStateCalculator, IGraphPathMinCostSearcher graphPathMinCostSearcher)
        {
            _board = board;
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _graphPathMinCostSearcher = graphPathMinCostSearcher;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var costsGraphBeforeMove = GetCostsGraph(move.Player);
            var friendlyBasesBeforeMove = GetFriendlyBasesPositions(move.Player);
            var enemiesPositionsBeforeMove = GetEnemiesPositions(move.Player);
            var minimalEnemyProximityBeforeMove = GetMinimalEnemyProximity(friendlyBasesBeforeMove, enemiesPositionsBeforeMove, costsGraphBeforeMove);
            
            var costsGraphAfterMove = GetCostsGraphAfterMove(move);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            var friendlyBasesAfterMove = new List<Vector2Int>();
            foreach (var basePos in friendlyBasesBeforeMove)
            {
                if (boardStateAfterMove[basePos.X, basePos.Y] == CellState.FriendlyBase)
                {
                    friendlyBasesAfterMove.Add(basePos);
                }
            }
            var enemiesPositionsAfterMove = new List<Vector2Int>();
            foreach (var enemyPos in enemiesPositionsBeforeMove)
            {
                if (boardStateAfterMove[enemyPos.X, enemyPos.Y] == CellState.Enemy || boardStateAfterMove[enemyPos.X, enemyPos.Y] == CellState.EnemyBase)
                {
                    enemiesPositionsAfterMove.Add(enemyPos);
                }
            }
            var minimalEnemyProximityAfterMove = GetMinimalEnemyProximity(friendlyBasesAfterMove, enemiesPositionsAfterMove, costsGraphAfterMove);
            
            return (minimalEnemyProximityAfterMove - minimalEnemyProximityBeforeMove) / (float)minimalEnemyProximityBeforeMove;
        }

        private int GetMinimalEnemyProximity(List<Vector2Int> friendlyBasesBeforeMove, List<Vector2Int> enemiesPositionsBeforeMove, int[,] costsGraphBeforeMove)
        {
            var minimalEnemyProximity = int.MaxValue;
            foreach (var friendlyBase in friendlyBasesBeforeMove)
            {
                foreach (var enemyPosition in enemiesPositionsBeforeMove)
                {
                    var costsGraphCopy = (int[,])costsGraphBeforeMove.Clone();
                    costsGraphCopy[enemyPosition.X, enemyPosition.Y] = 0;
                    costsGraphCopy[friendlyBase.X, friendlyBase.Y] = 0;
                    var distance = _graphPathMinCostSearcher.FindMinCost(costsGraphCopy,friendlyBase, enemyPosition);
                    if (distance < minimalEnemyProximity)
                    {
                        minimalEnemyProximity = distance;
                    }
                }
            }

            return minimalEnemyProximity;
        }

        private List<Vector2Int> GetEnemiesPositions(Player movePlayer)
        {
            var enemyUnitsPositions = new List<Vector2Int>();
            foreach (var cell in _board)
            {
                if (cell.HasContent(c => c is IPlayerOwned playerOwned && playerOwned.GetOwner() != movePlayer))
                {
                    enemyUnitsPositions.Add(cell.Position);
                }
            }
            
            return enemyUnitsPositions;
        }

        private List<Vector2Int> GetFriendlyBasesPositions(Player movePlayer)
        {
            var friendlyBasesPositions = new List<Vector2Int>();
            foreach (var cell in _board)
            {
                if (cell.HasContent(c => c is IPlayerOwned playerOwned && playerOwned.GetOwner() == movePlayer))
                {
                    if (cell.HasContent(c => c is CastleGO))
                    {
                        friendlyBasesPositions.Add(cell.Position);
                    }
                }
            }
            
            return friendlyBasesPositions;
        }

        private int[,] GetCostsGraphAfterMove(AbstractMove move)
        {
            var costsGraph = GetCostsGraph(move.Player);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            for (int i = 0; i < costsGraph.GetLength(0); i++)
            {
                for (int j = 0; j < costsGraph.GetLength(1); j++)
                {
                    if (boardStateAfterMove[i, j] == CellState.Friendly)
                    {
                        costsGraph[i, j] = 3;
                    }

                    if (boardStateAfterMove[i, j] == CellState.Free)
                    {
                        costsGraph[i, j] = 1;
                    }

                    if (boardStateAfterMove[i, j] == CellState.EnemyBase || boardStateAfterMove[i, j] == CellState.FriendlyBase)
                    {
                        costsGraph[i, j] = 1000;
                    }
                }
            }

            return costsGraph;
        }

        private int[,] GetCostsGraph(Player movePlayer)
        {
            var costsGraph = new int[_board.GetLength(0), _board.GetLength(1)];
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    var cell = _board[i, j];
                    costsGraph[i, j] = GetCellCost(cell, movePlayer);
                }
            }

            return costsGraph;
        }
        
        private int GetCellCost(Cell cell, Player movePlayer)
        {
            if (!cell.HasContent())
            {
                return 1;
            }
            if (cell.HasContent(c => c is IReplaceable))
            {
                var replaceable = cell.GetContent().Find(c => c is IReplaceable) as IReplaceable;
                if (replaceable is IPlayerOwned playerOwned && playerOwned.GetOwner() == movePlayer)
                {
                    return replaceable.GetReplaceCost();
                }
                return 1000;
            }
            if (cell.HasContent(c => c is ICapturable))
            {
                return 1000;
            }
            if (cell.HasContent(c => c is IRemovable))
            {
                var removable = cell.GetContent().Find(c => c is IRemovable) as IRemovable;
                return removable.CanBeRemoved() ? removable.GetRemoveCost() : int.MaxValue;
            }
            return 1000;
        }
    }
}