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
        }

        public virtual void ShowCurrentPlayer()
        {
            
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            
        }
    }
}