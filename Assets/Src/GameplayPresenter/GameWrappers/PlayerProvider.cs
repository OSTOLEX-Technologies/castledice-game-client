using castledice_game_logic;

namespace Src.GameplayPresenter.GameWrappers
{
    public class PlayerProvider : IPlayerProvider
    {
        private readonly Game _game;

        public PlayerProvider(Game game)
        {
            _game = game;
        }

        public Player GetPlayer(int playerId)
        {
            return _game.GetPlayer(playerId);
        }
    }
}