using Src.GameplayPresenter.Cells;
using Src.GameplayView.Grid;

namespace Src.GameplayView.Cells
{
    public class SquareCellsGenerator3D : ICellsGenerator
    {
        private readonly ISquareCellsFactory _cellsFactory;
        private readonly IGameObjectsGrid _grid;

        public SquareCellsGenerator3D(ISquareCellsFactory factory, IGameObjectsGrid grid)
        {
            _cellsFactory = factory;
            _grid = grid;
        }
    
        public void GenerateCells(CellViewData[,] cellsViewMap)
        {
            for (int i = 0; i < cellsViewMap.GetLength(0); i++)
            {
                for (int j = 0; j < cellsViewMap.GetLength(1); j++)
                {
                    var cellData = cellsViewMap[i, j];
                    if (cellData.IsNull) continue;
                    var cell = _cellsFactory.GetSquareCell(cellData.AssetId);
                    _grid.AddChild((i, j), cell);
                }
            }
        }
    }
}
