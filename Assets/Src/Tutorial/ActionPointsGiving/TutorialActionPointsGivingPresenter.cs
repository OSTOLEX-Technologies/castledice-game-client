using castledice_game_logic;
using Src.GameplayView.ActionPointsGiving;

namespace Src.Tutorial.ActionPointsGiving
{
    public class TutorialActionPointsGivingPresenter
    {
        private readonly IActionPointsGivingView _view;
        private readonly IActionPointsGenerator _actionPointsGenerator;
        private Game _game;

        public TutorialActionPointsGivingPresenter(IActionPointsGivingView view, IActionPointsGenerator actionPointsGenerator, Game game)
        {
            _view = view;
            _actionPointsGenerator = actionPointsGenerator;
            _game = game;
            _game.TurnSwitched += OnTurnSwitched;
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            var currentPlayer = game.GetCurrentPlayer();
            var actionPointsAmount = _actionPointsGenerator.GetActionPoints(currentPlayer);
            game.GiveActionPointsToPlayer(currentPlayer.Id, actionPointsAmount);
            _view.ShowActionPointsForPlayer(currentPlayer, actionPointsAmount);
        }
    }
}