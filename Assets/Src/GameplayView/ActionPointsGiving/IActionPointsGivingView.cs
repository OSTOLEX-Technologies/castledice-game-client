using castledice_game_logic;

namespace Src.GameplayView.ActionPointsGiving
{
    public interface IActionPointsGivingView
    {
        void ShowActionPointsForPlayer(Player player, int amount);
    }
}