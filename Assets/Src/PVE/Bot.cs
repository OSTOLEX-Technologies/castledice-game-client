using castledice_game_logic;
using Src.GameplayPresenter.GameWrappers;
using Src.PVE.BotTriggers;
using Src.PVE.MoveSearchers;

namespace Src.PVE
{
    public class Bot
    {
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IBestMoveSearcher _bestMoveSearcher;
        private readonly IBotMoveTrigger _moveTrigger;
        private readonly Player _botPlayer;
        
        public Bot(ILocalMoveApplier localMoveApplier, IBestMoveSearcher bestMoveSearcher, Player botPlayer, IBotMoveTrigger moveTrigger)
        {
            _localMoveApplier = localMoveApplier;
            _bestMoveSearcher = bestMoveSearcher;
            _botPlayer = botPlayer;
            _moveTrigger = moveTrigger;
            _moveTrigger.ShouldMakeMove += TryMakeMove;
        }
        
        private void TryMakeMove()
        {
            var bestMove = _bestMoveSearcher.GetBestMove(_botPlayer.Id);
            _localMoveApplier.ApplyMove(bestMove);
        }
    }
}