using castledice_game_logic;
using Src.GameplayPresenter.Cells;

namespace Src.GameplayView.Cells
{
    public class CellsView : ICellsView
    {
        private readonly ICellsGeneratorsFactory _cellsGeneratorsFactory;

        public CellsView(ICellsGeneratorsFactory cellsGeneratorsFactory)
        {
            _cellsGeneratorsFactory = cellsGeneratorsFactory;
        }
        
        public void GenerateCells(CellType cellType, CellViewData[,] cellViewMap)
        {
            var cellsGenerator = _cellsGeneratorsFactory.GetGenerator(cellType);
            cellsGenerator.GenerateCells(cellViewMap);
        }
    }
}