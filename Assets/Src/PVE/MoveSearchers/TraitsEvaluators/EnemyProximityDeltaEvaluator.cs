using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using NUnit.Framework;
using Src.PVE.TraitsEvaluators;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    /// <summary>
    /// This evaluator calculates the difference between the minimal distance from the friendly base to the enemy
    /// and the minimal distance from the friendly base to the enemy after the move.
    /// Method EvaluateTrait returns the percentage of the change in the minimal distance from the friendly base to the enemy.
    /// </summary>
    public class EnemyProximityDeltaEvaluator : IMoveTraitEvaluator
    {
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly Player _player;
        
        public EnemyProximityDeltaEvaluator(IBoardCellsStateCalculator boardCellsStateCalculator, Player player)
        {
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _player = player;
        }
        
        public float EvaluateTrait(AbstractMove move)
        {
            var currentBoardState = _boardCellsStateCalculator.GetCurrentBoardState(_player);
            var currentMinimalEnemyToBaseDistance = GetMinimalEnemyToBaseDistance(currentBoardState);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            var futureMinimalEnemyToBaseDistance = GetMinimalEnemyToBaseDistance(boardStateAfterMove);
            return (currentMinimalEnemyToBaseDistance - futureMinimalEnemyToBaseDistance ) / currentMinimalEnemyToBaseDistance;
        }

        private float GetMinimalEnemyToBaseDistance(CellState[,] boardState)
        {
            var friendlyBasesPositions = GetFriendlyBasesPositions(boardState);
            var enemyPositions = GetEnemyPositions(boardState);
            float minimalDistance = float.MaxValue;
            foreach (var friendlyBasePosition in friendlyBasesPositions)
            {
                foreach (var enemyPosition in enemyPositions)
                {
                    var distance = GetDistanceBetweenPositions(friendlyBasePosition, enemyPosition);
                    if (distance < minimalDistance)
                    {
                        minimalDistance = distance;
                    }
                }
            }
            
            return minimalDistance;
        }

        private float GetDistanceBetweenPositions(Vector2Int first, Vector2Int second)
        {
            return Mathf.Sqrt(Mathf.Pow((first.X - second.X), 2) + Mathf.Pow((first.Y - second.Y), 2));
        }

        private List<Vector2Int> GetEnemyPositions(CellState[,] boardState)
        {
            return GetPositionsForCellState(boardState, CellState.Enemy);
        }

        private List<Vector2Int> GetFriendlyBasesPositions(CellState[,] boardState)
        {
            return GetPositionsForCellState(boardState, CellState.FriendlyBase);
        }
        
        private List<Vector2Int> GetPositionsForCellState(CellState[,] boardState, CellState cellState)
        {
            var positions = new List<Vector2Int>();
            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                for (int j = 0; j < boardState.GetLength(1); j++)
                {
                    if (boardState[i, j] == cellState)
                    {
                        positions.Add(new Vector2Int(i, j));
                    }
                }
            }
            
            return positions;
        }
    }
}