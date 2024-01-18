using castledice_game_logic;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public interface IPlayerTimerViewProvider
    {
        IPlayerTimerView GetTimerViewForPlayer(Player player);
    }
}