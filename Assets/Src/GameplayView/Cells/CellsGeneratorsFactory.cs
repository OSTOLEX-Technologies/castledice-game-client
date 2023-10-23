using System;
using castledice_game_logic;
using UnityEngine;

namespace Src.GameplayView.Cells
{
    public class CellsGeneratorsFactory : ICellsGeneratorsFactory
    {
        private readonly SquareCellsGenerator3D _squareCellsGenerator3D;

        public CellsGeneratorsFactory(SquareCellsGenerator3D squareCellsGenerator)
        {
            _squareCellsGenerator3D = squareCellsGenerator;
        }
        
        public ICellsGenerator GetGenerator(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Square:
                    return _squareCellsGenerator3D;
                default:
                    throw new ArgumentException("Unfamiliar cell type: " + nameof(cellType));
            }
        }
    }
}
