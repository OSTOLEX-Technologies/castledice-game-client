using System;
using System.Threading.Tasks;
using castledice_game_logic;

namespace Src.PVE.BotTriggers
{
    public class OpportunityDelayedBotMoveTrigger : IBotMoveTrigger
    {
        private readonly Game _game;
        private readonly Player _botPlayer;
        private readonly int _delayMilliseconds;
        
        private bool BotCanMakeMove => _game.GetCurrentPlayer() == _botPlayer && _botPlayer.ActionPoints.Amount > 0;
        
        public event Action ShouldMakeMove;
        
        public OpportunityDelayedBotMoveTrigger(Game game, Player botPlayer, int delayMilliseconds)
        {
            _game = game;
            _botPlayer = botPlayer;
            _delayMilliseconds = delayMilliseconds;
            _game.MoveApplied += (_,_) => TryTriggerBot();
            _botPlayer.ActionPoints.ActionPointsIncreased += (_,_) => TryTriggerBot();
        }

        private async void TryTriggerBot()
        {
            if (!BotCanMakeMove) return;
            await Task.Delay(_delayMilliseconds);
            ShouldMakeMove?.Invoke();
        }
    }
}