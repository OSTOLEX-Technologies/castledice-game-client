using System;
using System.Threading.Tasks;
using castledice_game_logic;
using Src.GameplayPresenter.GameWrappers;
using Src.General.NumericSequences;
using Src.PVE.MoveSearchers;
using Src.TimeManagement;

namespace Src.PVE
{
    public class ConfigurableDelaysBot : Bot
    {
        private readonly IAsyncDelayer _delayer;
        private readonly IIntSequence _delaysSequenceMilliseconds;
        
        public ConfigurableDelaysBot(ILocalMoveApplier localMoveApplier, IBestMoveSearcher bestMoveSearcher, Game game, Player botPlayer, IAsyncDelayer delayer, IIntSequence delaysSequenceMilliseconds) : base(localMoveApplier, bestMoveSearcher, game, botPlayer)
        {
            _delayer = delayer;
            _delaysSequenceMilliseconds = delaysSequenceMilliseconds;
        }

        protected override Task Delay()
        {
            var delay = _delaysSequenceMilliseconds.Next();
            return _delayer.Delay(TimeSpan.FromMilliseconds(delay));
        }
    }
}