using Src.GameplayPresenter.Cells;
using Src.GameplayView.Grid;

namespace Src.GameplayView.Cells
{
    public class SquareCellsViewGenerator3D : ICellsViewGenerator
    {
        private readonly ISquareCellsFactory _cellsFactory;
        private readonly IGrid _grid;

        public SquareCellsViewGenerator3D(ISquareCellsFactory factory, IGrid grid)
        {
            _cellsFactory = factory;
            _grid = grid;
        }
    
        public void GenerateCellsView(CellViewData[,] cellsViewMap)
        {
            for (int i = 0; i < cellsViewMap.GetLength(0); i++)
            {
                for (int j = 0; j < cellsViewMap.GetLength(1); j++)
                {
                    var cellData = cellsViewMap[i, j];
                    if (cellData.IsNull) continue;
                    var cell = _cellsFactory.GetSquareCell(cellData.AssetId);
                    _grid.GetCell((i, j)).AddChild(cell);
                }
            }
        }
    }
}
