using castledice_game_logic;
using castledice_game_logic.GameObjects;
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
            foreach (var cell in board)
            {
                cell.ContentAdded += OnContentAdded;
                cell.ContentRemoved += OnContentRemoved;
                foreach (var content in cell.GetContent())
                {
                    content.StateModified += OnContentStateModified;
                }
            }
        }

        private void OnContentAdded(object sender, Content content)
        {
            _view.AddViewForContent(((Cell) sender).Position, content);
            content.StateModified += OnContentStateModified;
        }

        private void OnContentRemoved(object sender, Content content)
        {
            _view.RemoveViewForContent(content);
            content.StateModified -= OnContentStateModified;
        }
        
        private void OnContentStateModified(object sender, Content content)
        {
            _view.UpdateViewForContent(content);
        }
        
        ~CellsContentPresenter()
        {
            foreach (var cell in _board)
            {
                cell.ContentAdded -= OnContentAdded;
                cell.ContentRemoved -= OnContentRemoved;
                foreach (var content in cell.GetContent())
                {
                    content.StateModified -= OnContentStateModified;
                }
            }
        }
    }
}