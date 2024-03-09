using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class SimpleArmyStateCalculator : ISimpleArmyStateCalculator
    {
        private readonly IUnconnectedValuesCutter<SimpleCellState> _unconnectedValuesCutter;
        private readonly IUnitsPositionsSearcher _unitsPositionsSearcher;
        private readonly IBasePositionsCalculator _basePositionsCalculator;
        private readonly Vector2Int _boardSize;

        public SimpleArmyStateCalculator(IUnconnectedValuesCutter<SimpleCellState> unconnectedValuesCutter, IUnitsPositionsSearcher unitsPositionsSearcher, IBasePositionsCalculator basePositionsCalculator, Vector2Int boardSize)
        {
            if (boardSize.X <= 0 || boardSize.Y <= 0)
            {
                throw new ArgumentException("Board size dimensions must be greater than 0");
            }
            
            _unconnectedValuesCutter = unconnectedValuesCutter;
            _unitsPositionsSearcher = unitsPositionsSearcher;
            _basePositionsCalculator = basePositionsCalculator;
            _boardSize = boardSize;
        }

        public SimpleCellState[,] GetArmyState(Player forPlayer)
        {
            var result = new SimpleCellState[_boardSize.X, _boardSize.Y];
            SetEverythingToNeither(result);
            var unitsPositions = _unitsPositionsSearcher.GetUnitsPositions(forPlayer);
            SetValuesToPositions(result, unitsPositions, SimpleCellState.Unit);
            var basePositions = _basePositionsCalculator.GetBasePositions(forPlayer);
            SetValuesToPositions(result, basePositions, SimpleCellState.Base);
            return result;
        }

        public SimpleCellState[,] GetArmyStateAfterMove(Player forPlayer, AbstractMove afterMove)
        {
            var result = new SimpleCellState[_boardSize.X, _boardSize.Y];
            SetEverythingToNeither(result);
            var unitsPositions = _unitsPositionsSearcher.GetUnitsPositions(forPlayer);
            SetValuesToPositions(result, unitsPositions, SimpleCellState.Unit);
            var basePositions = _basePositionsCalculator.GetBasePositionsAfterMove(forPlayer, afterMove);
            SetValuesToPositions(result, basePositions, SimpleCellState.Base);
            if (afterMove is ReplaceMove replaceMove)
            {
                result[replaceMove.Position.X, replaceMove.Position.Y] = SimpleCellState.Neither;
            }
            _unconnectedValuesCutter.CutUnconnectedValues(result, SimpleCellState.Unit, SimpleCellState.Base, SimpleCellState.Neither);
            return result;
        }
        
        private static void SetEverythingToNeither(SimpleCellState[,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = SimpleCellState.Neither;
                }
            }
        }
        
        private static void SetValuesToPositions(SimpleCellState[,] array, List<Vector2Int> positions, SimpleCellState value)
        {
            foreach (var position in positions)
            {
                array[position.X, position.Y] = value;
            }
        }
    }
}