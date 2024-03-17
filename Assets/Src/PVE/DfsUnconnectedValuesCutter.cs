using System.Collections.Generic;
using castledice_game_logic.Math;
using Src.PVE.Calculators;

namespace Src.PVE
{
    public class DfsUnconnectedValuesCutter : IUnconnectedValuesCutter
    {
        private bool[,] _connectedCells;
        
        public void CutUnconnectedValues(
            SimpleCellState[,] inputMatrix, 
            SimpleCellState unitState, 
            SimpleCellState baseState, 
            SimpleCellState freeState)
        {
            _connectedCells = new bool[inputMatrix.GetLength(0), inputMatrix.GetLength(1)];
            var basePositions = GetBasePositions(inputMatrix, baseState);
            if (basePositions.Count == 0)
            {
                SetAllUnitCellsFree(inputMatrix, freeState, unitState);
            }
            foreach (var basePosition in basePositions)
            {
                MarkConnectedCells(basePosition.X, basePosition.Y, inputMatrix, baseState, unitState);
            }
            MarkUnknownCellsAsConnected(inputMatrix, unitState, baseState, freeState);
            SetNotConnectedCellsFree(inputMatrix, freeState);
        }

        private void MarkUnknownCellsAsConnected(
            SimpleCellState[,] matrixCopy, 
            SimpleCellState unitState, 
            SimpleCellState baseState, 
            SimpleCellState freeState)
        {
            for (int i = 0; i < matrixCopy.GetLength(0); i++)
            {
                for (int j = 0; j < matrixCopy.GetLength(1); j++)
                {
                    if (!matrixCopy[i, j].Equals(unitState) && !matrixCopy[i, j].Equals(baseState) && !matrixCopy[i, j].Equals(freeState))
                    {
                        _connectedCells[i, j] = true;
                    }
                }
            }
        }

        private void SetNotConnectedCellsFree(
            SimpleCellState[,] matrixCopy, 
            SimpleCellState freeState)
        {
            for (int i = 0; i < matrixCopy.GetLength(0); i++)
            {
                for (int j = 0; j < matrixCopy.GetLength(1); j++)
                {
                    if (!_connectedCells[i, j])
                    {
                        matrixCopy[i, j] = freeState;
                    }
                }
            }
        }

        private void MarkConnectedCells(
            int basePositionX, 
            int basePositionY, 
            SimpleCellState[,] inputMatrix, 
            SimpleCellState baseState, 
            SimpleCellState unitState)
        {
            _connectedCells[basePositionX, basePositionY] = true;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nextX = basePositionX + i;
                    int nextY = basePositionY + j;
                    var nextCellDoesNotExist = nextX < 0 || nextX >= inputMatrix.GetLength(0) || nextY < 0 || nextY >= inputMatrix.GetLength(1);
                    if (nextCellDoesNotExist || _connectedCells[nextX, nextY]) continue;
                    var current = inputMatrix[basePositionX, basePositionY];
                    var next = inputMatrix[nextX, nextY];
                    var currentIsBaseOrUnit = current.Equals(baseState) || current.Equals(unitState);
                    var nextIsBaseOrUnit = next.Equals(baseState) || next.Equals(unitState);
                    if (currentIsBaseOrUnit && 
                        nextIsBaseOrUnit)
                    {
                        MarkConnectedCells(nextX, nextY, inputMatrix, baseState, unitState);
                    }
                }
            }
        }
        
        private void SetAllUnitCellsFree(
            SimpleCellState[,] inputMatrix, 
            SimpleCellState freeState, 
            SimpleCellState unitState)
        {
            for (int i = 0; i < inputMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {
                    if (inputMatrix[i, j].Equals(unitState))
                    {
                        inputMatrix[i, j] = freeState;
                    }
                }
            }
        }

        private List<Vector2Int> GetBasePositions(
            SimpleCellState[,] inputMatrix, 
            SimpleCellState baseState)
        {
            var basePositions = new List<Vector2Int>();
            for (int i = 0; i < inputMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {
                    if (inputMatrix[i, j].Equals(baseState))
                    {
                        basePositions.Add(new Vector2Int(i, j));
                    }
                }
            }
            return basePositions;
        }
    }
}