using castledice_game_logic;
using Src.GameplayView.CellsContent;

namespace Src.GameplayPresenter.CellsContent
{
    public class CellsContentPresenter
    {
        private ICellsContentView _view;
        private Board _board;
        
        public CellsContentPresenter(ICellsContentView view, Board board)
        {
            _view = view;
            _board = board;
        }
    }
}