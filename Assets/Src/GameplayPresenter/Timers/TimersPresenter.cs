using System;
using castledice_game_logic;
using Src.GameplayView.Timers;

namespace Src.GameplayPresenter.Timers
{
    public class TimersPresenter : ITimersPresenter
    {
        private readonly ITimersView _timersView;
        private readonly Game _game;

        public TimersPresenter(ITimersView timersView, Game game)
        {
            _timersView = timersView;
            _game = game;
        }

        public void SwitchTimerForPlayer(int playerId, TimeSpan timeLeft, bool switchTo)
        {
            var player = _game.GetPlayer(playerId);
            player.Timer.SetTimeLeft(timeLeft);
            if (switchTo)
            {
                _timersView.StartTimer(player);
                player.Timer.Start();
            }
            else
            {
                _timersView.StopTimer(player);
                player.Timer.Stop();
            }
        }
    }
}