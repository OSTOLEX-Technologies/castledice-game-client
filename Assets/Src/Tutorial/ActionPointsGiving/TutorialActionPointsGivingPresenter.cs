using castledice_game_logic;

namespace Src.Tutorial.ActionPointsGiving
{
    public class TutorialActionPointsGivingPresenter
    {
        private readonly IActionPointsGenerator _actionPointsGenerator;
        private readonly Game _game;

        public TutorialActionPointsGivingPresenter(IActionPointsGenerator actionPointsGenerator, Game game)
        {
            _actionPointsGenerator = actionPointsGenerator;
            _game = game;
            _game.TurnSwitched += OnTurnSwitched;
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            GiveActionPointsToCurrentPlayer();
        }

        public void GiveActionPointsToCurrentPlayer()
        {
            var currentPlayer = _game.GetCurrentPlayer();
            var actionPointsAmount = _actionPointsGenerator.GetActionPoints(currentPlayer);
            _game.GiveActionPointsToPlayer(currentPlayer.Id, actionPointsAmount);
        }
    }
}