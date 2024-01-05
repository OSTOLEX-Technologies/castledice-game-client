using castledice_game_logic;
using Src.GameplayView.Timers.PlayerTimerViews;

namespace Src.GameplayView.Timers
{
    public interface IPlayerTimerViewProvider
    {
        IPlayerTimerView GetTimerViewForPlayer(Player player);
    }
}