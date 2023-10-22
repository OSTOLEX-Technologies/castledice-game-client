using castledice_game_data_logic;
using Src.GameplayView.Grid;

namespace Src.GameplayPresenter.Grid
{
    public class GridPresenter
    {
        private IGridView _view;
        private GameStartData _gameStartData;
        
        public GridPresenter(IGridView view, GameStartData gameStartData)
        {
            _view = view;
            _gameStartData = gameStartData;
        }
        
        public void GenerateGrid()
        {
            _view.GenerateGrid(_gameStartData.CellType, _gameStartData.CellsPresence);
        }
    }
}