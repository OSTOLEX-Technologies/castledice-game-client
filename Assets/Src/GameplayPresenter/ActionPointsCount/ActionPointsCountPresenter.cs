using castledice_game_logic;
using Src.GameplayView.ActionPointsCount;

namespace Src.GameplayPresenter.ActionPointsCount
{
    public class ActionPointsCountPresenter
    {
        private readonly IPlayerDataProvider _playerDataProvider;
        private readonly Game _game;
        private readonly IActionPointsCountView _view;

        public ActionPointsCountPresenter(IPlayerDataProvider playerDataProvider, Game game, IActionPointsCountView view)
        {
            _playerDataProvider = playerDataProvider;
            _game = game;
            _view = view;
            SubscribeToPlayerEvents();
            SubscribeToGameEvents();
        }

        private void SubscribeToPlayerEvents()
        {
            var player = _game.GetPlayer(_playerDataProvider.GetIdAsync());
            player.ActionPoints.ActionPointsIncreased += OnActionPointsModified;
            player.ActionPoints.ActionPointsDecreased += OnActionPointsModified;
        }

        private void SubscribeToGameEvents()
        {
            _game.TurnSwitched += OnTurnSwitched;
        }

        public virtual void UpdateActionPointsCount()
        {
            var playerId = _playerDataProvider.GetIdAsync();
            if (!IsCurrentPlayer(playerId))
            {
                _view.HideActionPointsCount();
            }
            else
            {
                var player = _game.GetPlayer(playerId);
                var actionPointsCount = player.ActionPoints.Amount;
                _view.ShowActionPointsCount(actionPointsCount);
            }
        }
        
        private bool IsCurrentPlayer(int playerId)
        {
            var currentPlayer = _game.GetCurrentPlayer();
            return currentPlayer.Id == playerId;
        }

        private void OnActionPointsModified(object sender, int amount)
        {
            UpdateActionPointsCount();
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            UpdateActionPointsCount();
        }
    }
}