using castledice_game_logic;

namespace Src.Tutorial.ActionPointsGiving
{
    public interface IActionPointsGenerator
    {
        int GetActionPoints(Player forPlayer);
    }
}