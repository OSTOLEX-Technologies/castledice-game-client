using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.GameWrappers
{
    public class LocalMovesApplier : ILocalMoveApplier
    {
        private readonly Game _game;
        
        public LocalMovesApplier(Game game)
        {
            _game = game;
        }
        
        public void ApplyMove(AbstractMove move)
        {
            _game.TryMakeMove(move);
        }
    }
}