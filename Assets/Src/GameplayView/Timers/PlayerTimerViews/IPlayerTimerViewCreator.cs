using castledice_game_logic;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public interface IPlayerTimerViewCreator
    {
        IPlayerTimerView Create(Player player);
    }
}