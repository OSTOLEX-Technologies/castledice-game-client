using castledice_game_logic;
using castledice_game_logic.GameObjects;
using Src.GameplayView.PlacedUnitsHighlights;

namespace Src.GameplayPresenter.PlacedUnitsHighlights
{
    public class PlacedUnitsHighlightsPresenter
    {
        private readonly Board _board;
        private readonly IPlacedUnitsHighlightsView _view;
        
        public PlacedUnitsHighlightsPresenter(Board board, IPlacedUnitsHighlightsView view)
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
                _view.ShowHighlight(cell.Position, playerOwned.GetOwner());
            }
        }

        private void OnContentRemoved(object sender, Content content)
        {
            if (content is IPlayerOwned playerOwned)
            {
                var cell = (Cell) sender;
                _view.HideHighlight(cell.Position);
            }
        }
        
        ~PlacedUnitsHighlightsPresenter()
        {
            foreach (var cell in _board)
            {
                cell.ContentAdded -= OnContentAdded;
                cell.ContentRemoved -= OnContentRemoved;
            }
        }
    }
}