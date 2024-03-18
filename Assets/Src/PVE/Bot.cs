using System.Threading.Tasks;
using castledice_game_logic;
using Src.GameplayPresenter.GameWrappers;
using Src.PVE.MoveSearchers;

namespace Src.PVE
{
    public abstract class Bot
    {
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IBestMoveSearcher _bestMoveSearcher;
        private readonly Game _game;
        private readonly Player _botPlayer;

        private bool BotCanMove => _game.GetCurrentPlayer() == _botPlayer && _botPlayer.ActionPoints.Amount > 0;
        
        protected Bot(ILocalMoveApplier localMoveApplier, IBestMoveSearcher bestMoveSearcher, Game game, Player botPlayer)
        {
            _localMoveApplier = localMoveApplier;
            _bestMoveSearcher = bestMoveSearcher;
            _game = game;
            _botPlayer = botPlayer;
            _game.MoveApplied += (sender, move) => TryMakeMove();
            _botPlayer.ActionPoints.ActionPointsIncreased += (sender, args) => TryMakeMove();
        }
        
        protected virtual async Task TryMakeMove()
        {
            if (!BotCanMove) return;
            var bestMove = _bestMoveSearcher.GetBestMove(_botPlayer.Id);
            await Delay();
            _localMoveApplier.ApplyMove(bestMove);
        }

        protected abstract Task Delay();
    }
}