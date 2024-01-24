using castledice_game_logic;
using castledice_game_logic.GameObjects;
using Src.GameplayView.UnitsUnderlines;

namespace Src.GameplayPresenter.UnitsUnderlines
{
    public class UnitsUnderlinesPresenter
    {
        private readonly Board _board;
        private readonly IUnitsUnderlinesView _view;
        
        public UnitsUnderlinesPresenter(Board board, IUnitsUnderlinesView view)
        {
            _board = board;
            _view = view;
            foreach (var cell in _board)
            {
                cell.ContentAdded += OnContentAdded;
                cell.ContentRemoved += OnContentRemoved;
            }
        }

        private void OnContentAdded(object sender, Content content)
        {
            if (content is IPlayerOwned playerOwned)
            {
                var cell = (Cell) sender;
                _view.ShowUnderline(cell.Position, playerOwned.GetOwner());
            }
        }

        private void OnContentRemoved(object sender, Content content)
        {
            if (content is IPlayerOwned playerOwned)
            {
                var cell = (Cell) sender;
                _view.HideUnderline(cell.Position);
            }
        }
    }
}