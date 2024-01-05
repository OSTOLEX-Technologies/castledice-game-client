using castledice_game_logic;
using Src.GameplayView.Updatables;

namespace Src.GameplayView.Timers
{
    public class TimersView : ITimersView
    {
        private readonly IPlayerTimerViewProvider _playerTimerViewProvider;
        private readonly IUpdater _updater;
        
        public TimersView(IPlayerTimerViewProvider playerTimerViewProvider, IUpdater updater)
        {
            _playerTimerViewProvider = playerTimerViewProvider;
            _updater = updater;
        }
        
        public void StartTimer(Player player)
        {
            var playerTimerView = _playerTimerViewProvider.GetTimerViewForPlayer(player);
            _updater.AddUpdatable(playerTimerView);
        }

        public void StopTimer(Player player)
        {
            var playerTimerView = _playerTimerViewProvider.GetTimerViewForPlayer(player);
            _updater.RemoveUpdatable(playerTimerView);
        }
    }
}