using System;
using castledice_game_logic;

namespace Src.GameplayView.Grid.GridGeneration
{
    public class GridGeneratorsFactory : IGridGeneratorsFactory
    {
        private readonly UnitySquareGridGenerator _squareGridGenerator;

        public GridGeneratorsFactory(UnitySquareGridGenerator squareGridGenerator)
        {
            _squareGridGenerator = squareGridGenerator;
        }

        public IGridGenerator GetGridGenerator(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Square:
                    return _squareGridGenerator;
                default:
                    throw new ArgumentOutOfRangeException("Unfamiliar cell type " + cellType);
            }
        }
    }
}