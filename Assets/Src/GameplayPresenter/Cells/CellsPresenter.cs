using castledice_game_data_logic;
using Src.GameplayView.Cells;

namespace Src.GameplayPresenter.Cells
{
    public class CellsPresenter
    {
        private ICellViewMapProvider _cellViewMapProvider;
        private ICellsView _cellsView;
        private GameStartData _gameStartData;

        public CellsPresenter(ICellViewMapProvider cellViewMapProvider, ICellsView cellsView, GameStartData gameStartData)
        {
            _cellViewMapProvider = cellViewMapProvider;
            _cellsView = cellsView;
            _gameStartData = gameStartData;
        }

        public void GenerateCells()
        {
            var cellViewMap = _cellViewMapProvider.GetCellViewMap(_gameStartData);
            _cellsView.GenerateCells(_gameStartData.CellType, cellViewMap);
        }
    }
}