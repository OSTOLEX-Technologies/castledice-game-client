using castledice_game_logic;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public interface IPlayerTimeViewProvider
    {
        TimeView GetTimeView(Player player);
    }
}