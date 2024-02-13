using castledice_game_logic;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public interface ITimeViewForPlayerProvider
    {
        TimeView GetTimeView(Player player);
    }
}