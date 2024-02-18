using System.Collections.Generic;
using castledice_game_logic.Math;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class DfsUnconnectedValuesCutter<T> : IUnconnectedValuesCutter<T>
    {
        private bool[,] _connectedCells;
        
        public T[,] CutUnconnectedUnits(T[,] inputMatrix, T unitState, T baseState, T freeState)
        {
            _connectedCells = new bool[inputMatrix.GetLength(0), inputMatrix.GetLength(1)];
            var matrixCopy = (T[,])inputMatrix.Clone();
            var basePositions = GetBasePositions(matrixCopy, baseState);
            if (basePositions.Count == 0)
            {
                SetAllUnitCellsFree(matrixCopy, freeState, unitState);
                return matrixCopy;
            }
            foreach (var basePosition in basePositions)
            {
                MarkConnectedCells(basePosition.X, basePosition.Y, matrixCopy, baseState, unitState);
            }
            MarkUnknownCellsAsConnected(matrixCopy, unitState, baseState, freeState);
            SetNotConnectedCellsFree(matrixCopy, freeState);
            return matrixCopy;
        }

        private void MarkUnknownCellsAsConnected(T[,] matrixCopy, T unitState, T baseState, T freeState)
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

        private void SetNotConnectedCellsFree(T[,] matrixCopy, T freeState)
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

        private void MarkConnectedCells(int basePositionX, int basePositionY, T[,] inputMatrix, T baseState, T unitState)
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
        
        private void SetAllUnitCellsFree(T[,] inputMatrix, T freeState, T unitState)
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

        private List<Vector2Int> GetBasePositions(T[,] inputMatrix, T baseState)
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