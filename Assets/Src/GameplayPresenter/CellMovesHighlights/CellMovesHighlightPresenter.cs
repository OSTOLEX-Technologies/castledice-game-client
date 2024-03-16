using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.CellMovesHighlights;

namespace Src.GameplayPresenter.CellMovesHighlights
{
    public class CellMovesHighlightPresenter
    {
        private readonly Player _localPlayer;
        private readonly ICellMovesListProvider _cellMovesListProvider;
        private readonly ICellMovesHighlightObserver _observer;
        private readonly ICellMovesHighlightView _view;

        public CellMovesHighlightPresenter(Player localPlayer, ICellMovesListProvider cellMovesListProvider, ICellMovesHighlightObserver observer, ICellMovesHighlightView view)
        {
            _localPlayer = localPlayer;
            _cellMovesListProvider = cellMovesListProvider;
            _observer = observer;
            _view = view;
            _observer.TimeToHighlight += HighlightCellMoves;
            _observer.TimeToHide += HideHighlights;
        }

        private void HighlightCellMoves()
        {
            _view.HideHighlights();
            var cellMovesList = _cellMovesListProvider.GetCellMovesList(_localPlayer.Id);
            _view.HighlightCellMoves(cellMovesList);
        }

        private void HideHighlights()
        {
            _view.HideHighlights();
        }
    }
}