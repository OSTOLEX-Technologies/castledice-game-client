using castledice_game_data_logic;
using Src.GameplayView;

namespace Src.GameplayPresenter.Cells
{
    public class CellsPresenter
    {
        private ICellViewMapProvider _cellViewMapProvider;
        private CellsView _cellsView;

        public CellsPresenter(ICellViewMapProvider cellViewMapProvider, CellsView cellsView)
        {
            _cellViewMapProvider = cellViewMapProvider;
            _cellsView = cellsView;
        }

        public void GenerateCells()
        {
            var gameStartData = Singleton<GameStartData>.Instance;
            var cellViewMap = _cellViewMapProvider.GetCellViewMap(gameStartData);
            _cellsView.GenerateCells(gameStartData.CellType, cellViewMap);
        }
    }
}