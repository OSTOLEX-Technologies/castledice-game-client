using castledice_game_logic;

namespace Src.GameplayPresenter.GameWrappers
{
    public class ActionPointsGiver : IActionPointsGiver
    {
        private readonly Game _game;
        
        public ActionPointsGiver(Game game)
        {
            _game = game;
        }
        
        public void GiveActionPoints(int playerId, int amount)
        {
            _game.GiveActionPointsToPlayer(playerId, amount);
        }
    }
}