using System;
using System.Collections.Generic;
using System.Linq;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.PVE
{
    public class DuelAggressiveMoveSearcher : IBestMoveSearcher
    {
        private readonly Board _board;
        private readonly Vector2Int _enemyCastlePosition;
        private readonly Vector2Int _botCastlePosition;
        private readonly int _botPlayerId;
        private readonly float _defensiveDistance;

        public DuelAggressiveMoveSearcher(Board board, Vector2Int enemyCastlePosition, Vector2Int botCastlePosition, int botPlayerId, float defensiveDistance)
        {
            _board = board;
            _enemyCastlePosition = enemyCastlePosition;
            _botCastlePosition = botCastlePosition;
            _botPlayerId = botPlayerId;
            _defensiveDistance = defensiveDistance;
        }

        public AbstractMove GetBestMove(List<AbstractMove> moves)
        {
            var enemyToOwnCastleDistance = GetDistanceToClosestEnemyUnit();
            if (enemyToOwnCastleDistance < _defensiveDistance)
            {
                return GetMostDefensiveMove(moves);
            }
            else
            {
                return GetMostAggressiveMove(moves);
            }
        }

        private float GetDistanceToClosestEnemyUnit()
        {
            var closestDistance = float.MaxValue;
            foreach (var cell in _board)        
            {
                if (cell.HasContent( c => c is IPlayerOwned && ((IPlayerOwned) c).GetOwner().Id != _botPlayerId))
                {
                    var distance = GetDistanceBetweenVectors(_botCastlePosition, cell.Position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                    }
                }   
            }

            return closestDistance;
        }
        
        private AbstractMove GetMostAggressiveMove(List<AbstractMove> moves)
        {
            if (moves.Count == 0)
            {
                throw new ArgumentException();
            }
            var moveToDistanceToEnemyCastle = new Dictionary<AbstractMove, float>();
            foreach (var move in moves)
            {
                var distance = GetDistanceToEnemyCastle(move);
                moveToDistanceToEnemyCastle.Add(move, distance);
            }

            var bestMove = moveToDistanceToEnemyCastle.First();
            foreach (var move in moveToDistanceToEnemyCastle)
            {
                if (move.Value < bestMove.Value)
                {
                    bestMove = move;
                }
            }

            return bestMove.Key;
        }
        
        private AbstractMove GetMostDefensiveMove(List<AbstractMove> moves)
        {
            if (moves.Count == 0)
            {
                throw new ArgumentException();
            }
            var moveToDistanceToOwnCastle = new Dictionary<AbstractMove, float>();
            foreach (var move in moves)
            {
                var distance = GetDistanceToOwnCastle(move);
                moveToDistanceToOwnCastle.Add(move, distance);
            }

            var bestMove = moveToDistanceToOwnCastle.First();
            foreach (var move in moveToDistanceToOwnCastle)
            {
                if (move.Value < bestMove.Value)
                {
                    bestMove = move;
                }
            }

            return bestMove.Key;
        }

        private float GetDistanceToEnemyCastle(AbstractMove move)
        {
            return GetDistanceBetweenVectors(_enemyCastlePosition, move.Position);
        }
        
        private float GetDistanceToOwnCastle(AbstractMove move)
        {
            return GetDistanceBetweenVectors(_botCastlePosition, move.Position);
        }
        
        private float GetDistanceBetweenVectors(Vector2Int vector1, Vector2Int vector2)
        {
            var vector = new Vector2Int(vector1.X - vector2.X, vector1.Y - vector2.Y);
            var distance = Mathf.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return distance;
        }
    }
}