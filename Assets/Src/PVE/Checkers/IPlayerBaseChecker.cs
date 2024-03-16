using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace Src.PVE.Checkers
{
    public interface IPlayerBaseChecker
    {
        bool IsPlayerBase(Content content, Player owner);
    }
}