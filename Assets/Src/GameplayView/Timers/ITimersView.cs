using castledice_game_logic;

namespace Src.GameplayView.Timers
{
    public interface ITimersView
    {
        void StartTimerForPlayer(Player player);
        void StopTimerForPlayer(Player player);
    }
}