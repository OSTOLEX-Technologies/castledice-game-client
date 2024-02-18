using System.Collections.Generic;
using System.Linq;
using castledice_game_logic.Math;
using Src.PVE.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class UnitsStructureCalculator : IUnitsStructureCalculator
    {
        private readonly IUnconnectedValuesCutter<CellState> _unconnectedValuesCutter;
        private readonly int _depth;

        public UnitsStructureCalculator(IUnconnectedValuesCutter<CellState> unconnectedValuesCutter, int depth = 1)
        {
            _depth = depth;
            _unconnectedValuesCutter = unconnectedValuesCutter;
        }

        public float CalculateFriendlyStructure(CellState[,] boardState)
        {
            return CalculateStructure(boardState, CellState.Friendly, CellState.FriendlyBase, CellState.Free, _depth);
        }
        
        public float CalculateEnemyStructure(CellState[,] boardState)
        {
            return CalculateStructure(boardState, CellState.Enemy, CellState.EnemyBase, CellState.Free, _depth);
        }

        private float CalculateStructure(CellState[,] inputMatrix, CellState unitState, CellState baseState,
            CellState freeState, int depth = 1)
        {
            var friendlyPositions = GetUnitsPositions(inputMatrix, unitState);
            if (friendlyPositions.Count == 0)
            {
                return 0;
            }
            var unitsStructures = new List<float>();
            foreach (var position in friendlyPositions)
            {
                var loss = GetStructureForUnitOnPosition(position, inputMatrix, unitState, baseState, freeState, depth);
                unitsStructures.Add(loss);
            }
            return unitsStructures.Sum() / friendlyPositions.Count;
        }

        private float GetStructureForUnitOnPosition(Vector2Int position, CellState[,] inputMatrix, CellState unitState, CellState baseState, CellState freeState, int depth = 1)
        {
            if (depth == 1)
            {
                var matrixCopy = (CellState[,])inputMatrix.Clone();
                var unitsCountBefore = GetUnitsPositions(matrixCopy, unitState).Count;
                matrixCopy[position.X, position.Y] = freeState;
                var matrixAfterCut =
                    _unconnectedValuesCutter.CutUnconnectedUnits(matrixCopy, unitState, baseState, freeState);
                var unitsCountAfter = GetUnitsPositions(matrixAfterCut, unitState).Count;
                return 1 - (unitsCountBefore - unitsCountAfter) / (float)unitsCountBefore;
            }
            else
            {
                var matrixCopy = (CellState[,])inputMatrix.Clone();
                var unitsCountBefore = GetUnitsPositions(matrixCopy, unitState).Count;
                var structureBefore = CalculateStructure(matrixCopy, unitState, baseState, freeState, depth - 1);
                matrixCopy[position.X, position.Y] = freeState;
                var matrixAfterCut =
                    _unconnectedValuesCutter.CutUnconnectedUnits(matrixCopy, unitState, baseState, freeState);
                var unitsCountAfter = GetUnitsPositions(matrixAfterCut, unitState).Count;
                var structureAfter = CalculateStructure(matrixAfterCut, unitState, baseState, freeState, depth - 1);
                if (structureBefore == 0)
                {
                    return 0;
                }

                var structureLoss = (structureBefore - structureAfter) / structureBefore;
                var unitsCountLoss = (unitsCountBefore - unitsCountAfter) / (float)unitsCountBefore;
                return 1 - structureLoss * unitsCountLoss;
            }
        }
        
        private List<Vector2Int> GetUnitsPositions(CellState[,] inputMatrix, CellState unitState)
        {
            var positions = new List<Vector2Int>();
            for (int i = 0; i < inputMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {
                    if (inputMatrix[i, j] == unitState)
                    {
                        positions.Add(new Vector2Int(i, j));
                    }
                }
            }

            return positions;
        }
    }
}