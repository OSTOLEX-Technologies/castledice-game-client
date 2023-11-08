using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.CellMovesHighlights;

namespace Src.GameplayPresenter.CellMovesHighlights
{
    public class CellMovesHighlightPresenter
    {
        private readonly IPlayerDataProvider _playerDataProvider;
        private readonly ICellMovesListProvider _cellMovesListProvider;
        private readonly Game _game;
        private readonly ICellMovesHighlightView _view;

        public CellMovesHighlightPresenter(IPlayerDataProvider playerDataProvider, ICellMovesListProvider cellMovesListProvider, Game game, ICellMovesHighlightView view)
        {
            _playerDataProvider = playerDataProvider;
            _cellMovesListProvider = cellMovesListProvider;
            _game = game;
            _view = view;
            _game.MoveApplied += OnMoveApplied;
            _game.TurnSwitched += OnTurnSwitched;
        }

        public virtual void HighlightCellMoves()
        {
            _view.HideHighlights();
            var playerId = _playerDataProvider.GetId();
            var cellMovesList = _cellMovesListProvider.GetCellMovesList(playerId);
            _view.HighlightCellMoves(cellMovesList);
        }

        private void OnMoveApplied(object sender, AbstractMove move)
        {
            HighlightCellMoves();
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            HighlightCellMoves();
        }
    }
}