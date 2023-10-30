using castledice_game_data_logic.ConfigsData;
using Src.GameplayView.Grid;

namespace Src.GameplayPresenter.Grid
{
    public class GridPresenter
    {
        private IGridView _view;
        private BoardData _boardData;
        
        public GridPresenter(IGridView view, BoardData boardData)
        {
            _view = view;
            _boardData = boardData;
        }
        
        public void GenerateGrid()
        {
            _view.GenerateGrid(_boardData.CellType, _boardData.CellsPresence);
        }
    }
}