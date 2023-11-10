using castledice_game_logic;
using Src.GameplayView.CurrentPlayer;

namespace Src.GameplayPresenter.CurrentPlayer
{
    public class CurrentPlayerPresenter
    {
        private readonly Game _game;
        private readonly ICurrentPlayerView _view;

        public CurrentPlayerPresenter(Game game, ICurrentPlayerView view)
        {
            _game = game;
            _view = view;
            _game.TurnSwitched += OnTurnSwitched;
        }

        public virtual void ShowCurrentPlayer()
        {
            _view.ShowCurrentPlayer(_game.GetCurrentPlayer());
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            ShowCurrentPlayer();
        }
    }
}