using castledice_game_logic;
using Src.GameplayPresenter.CellsGeneration;

namespace Src.GameplayView.Cells
{
    public class CellsView : ICellsView
    {
        private readonly ICellsViewGeneratorsFactory _cellsViewGeneratorsFactory;

        public CellsView(ICellsViewGeneratorsFactory cellsViewGeneratorsFactory)
        {
            _cellsViewGeneratorsFactory = cellsViewGeneratorsFactory;
        }
        
        public void GenerateCells(CellType cellType, CellViewData[,] cellViewMap)
        {
            var cellsGenerator = _cellsViewGeneratorsFactory.GetGenerator(cellType);
            cellsGenerator.GenerateCellsView(cellViewMap);
        }
    }
}