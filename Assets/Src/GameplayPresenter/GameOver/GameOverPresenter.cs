using castledice_game_logic;
using Src.GameplayView.GameOver;

namespace Src.GameplayPresenter.GameOver
{
    public class GameOverPresenter
    {
        private readonly Game _game;
        private readonly IGameOverView _view;

        public GameOverPresenter(Game game, IGameOverView view)
        {
            _game = game;
            _view = view;
            _game.Win += OnWin;
            _game.Draw += OnDraw;
        }

        private void OnWin(object sender, (Game game, Player winner) args)
        {
            _view.ShowWin(args.winner);
        }

        private void OnDraw(object sender, Game game)
        {
            _view.ShowDraw();
        }
    }
}