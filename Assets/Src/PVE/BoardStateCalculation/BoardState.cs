using System;
using System.Collections.Generic;
using castledice_game_logic.Math;

namespace Src.PVE.BoardStateCalculation
{
    public class BoardState
    {
        private CellState[,] _cellStates;
        
        public BoardState(CellState[,] cellStates)
        {
            _cellStates = cellStates;
        }

        public CellState this[int x, int y]
        {
            get => _cellStates[x, y];
            set => _cellStates[x, y] = value;
        }
        
        public int CountCellsWithState(CellState state)
        {
            int count = 0;
            for (int i = 0; i < _cellStates.GetLength(0); i++)
            {
                for (int j = 0; j < _cellStates.GetLength(1); j++)
                {
                    if (_cellStates[i, j] == state)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public List<Vector2Int> GetPositionsWithState(CellState state)
        {
            var positions = new List<Vector2Int>();
            for (int i = 0; i < _cellStates.GetLength(0); i++)
            {
                for (int j = 0; j < _cellStates.GetLength(1); j++)
                {
                    if (_cellStates[i, j] == state)
                    {
                        positions.Add(new Vector2Int(i, j));
                    }
                }
            }
            return positions;
        }
    }
}