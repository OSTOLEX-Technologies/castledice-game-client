using System.Collections.Generic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    public class BoardStateDistancesCalculator : IBoardStateDistancesCalculator
    {
        public float GetMinimalDistanceBetweenCellStates(CellState[,] boardState, CellState firstCellState,
            CellState secondCellState)
        {
            var firstStatePositions = GetPositionsForCellState(boardState, firstCellState);
            var secondStatePositions = GetPositionsForCellState(boardState, secondCellState);
            var minimalDistance = float.MaxValue;
            foreach (var friendlyBasePosition in firstStatePositions)
            foreach (var enemyPosition in secondStatePositions)
            {
                var distance = GetDistanceBetweenPositions(friendlyBasePosition, enemyPosition);
                if (distance < minimalDistance) minimalDistance = distance;
            }

            return minimalDistance;
        }

        private List<Vector2Int> GetPositionsForCellState(CellState[,] boardState, CellState cellState)
        {
            var positions = new List<Vector2Int>();
            for (var i = 0; i < boardState.GetLength(0); i++)
            for (var j = 0; j < boardState.GetLength(1); j++)
                if (boardState[i, j] == cellState)
                    positions.Add(new Vector2Int(i, j));

            return positions;
        }

        private float GetDistanceBetweenPositions(Vector2Int first, Vector2Int second)
        {
            return Mathf.Sqrt(Mathf.Pow(first.X - second.X, 2) + Mathf.Pow(first.Y - second.Y, 2));
        }
    }
}