using castledice_game_data_logic.Content.Placeable;
using Src.GameplayView.Cells;

namespace Src.GameplayPresenter.Cells
{
    public class CellsPresenter
    {
        private readonly ICellViewMapProvider _cellViewMapProvider;
        private readonly ICellsView _cellsView;
        private readonly BoardData _boardData;

        public CellsPresenter(ICellViewMapProvider cellViewMapProvider, ICellsView cellsView, BoardData boardData)
        {
            _cellViewMapProvider = cellViewMapProvider;
            _cellsView = cellsView;
            _boardData = boardData;
        }

        public void GenerateCells()
        {
            var cellViewMap = _cellViewMapProvider.GetCellViewMap(_boardData);
            _cellsView.GenerateCells(_boardData.CellType, cellViewMap);
        }
    }
}