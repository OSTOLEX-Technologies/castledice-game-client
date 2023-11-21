using System;
using castledice_game_logic;

namespace Src.GameplayView.Cells
{
    public class CellsViewGeneratorsFactory : ICellsViewGeneratorsFactory
    {
        private readonly SquareCellsViewGenerator3D _squareCellsViewGenerator3D;

        public CellsViewGeneratorsFactory(SquareCellsViewGenerator3D squareCellsViewGenerator)
        {
            _squareCellsViewGenerator3D = squareCellsViewGenerator;
        }
        
        public ICellsViewGenerator GetGenerator(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Square:
                    return _squareCellsViewGenerator3D;
                default:
                    throw new ArgumentException("Unfamiliar cell type: " + nameof(cellType));
            }
        }
    }
}
