using castledice_game_logic;

namespace Src.GameplayView.Timers
{
    public interface ITimersView
    {
        void StartTimer(Player player);
        void StopTimer(Player player);
    }
}