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
            SubscribeToGameEvents();
            SubscribeToPlayerEvents();
        }

        private void SubscribeToGameEvents()
        {
            _game.MoveApplied += OnMoveApplied;
            _game.TurnSwitched += OnTurnSwitched;
        }
        
        private void SubscribeToPlayerEvents()
        {
            var playerId = _playerDataProvider.GetId();
            var player = _game.GetPlayer(playerId);
            player.ActionPoints.ActionPointsIncreased += OnActionPointsGiven;
        }

        public virtual void HighlightCellMoves()
        {
            _view.HideHighlights();
            var playerId = _playerDataProvider.GetId();
            if (!IsCurrentPlayer(playerId)) return;
            var cellMovesList = _cellMovesListProvider.GetCellMovesList(playerId);
            _view.HighlightCellMoves(cellMovesList);
        }

        private bool IsCurrentPlayer(int playerId)
        {
            var currentPlayer = _game.GetCurrentPlayer();
            return currentPlayer.Id == playerId;
        }

        private void OnMoveApplied(object sender, AbstractMove move)
        {
            HighlightCellMoves();
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            HighlightCellMoves();
        }

        private void OnActionPointsGiven(object sender, int amount)
        {
            HighlightCellMoves();
        }
    }
}