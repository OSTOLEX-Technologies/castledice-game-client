using System;
using System.Threading.Tasks;
using castledice_game_logic;
using Src.GameplayPresenter.GameWrappers;
using Src.PVE.MoveSearchers;
using Src.TimeManagement;

namespace Src.PVE
{
    public class SteadyBot : Bot
    {
        private readonly IAsyncDelayer _delayer;
        private readonly TimeSpan _delay;
        
        public SteadyBot(ILocalMoveApplier localMoveApplier, IBestMoveSearcher bestMoveSearcher, Game game, Player botPlayer, TimeSpan delay, IAsyncDelayer delayer) : base(localMoveApplier, bestMoveSearcher, game, botPlayer)
        {
            _delay = delay;
            _delayer = delayer;
        }

        protected override Task Delay()
        {
            return _delayer.Delay(_delay);
        }
    }
}