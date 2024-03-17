using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace Src.PVE.Checkers
{
    public interface IPlayerUnitChecker
    {
        bool IsPlayerUnit(Content content, Player owner);
    }
}