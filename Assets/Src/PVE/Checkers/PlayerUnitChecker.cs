using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace Src.PVE.Checkers
{
    public class PlayerUnitChecker : IPlayerUnitChecker
    {
        public bool IsPlayerUnit(Content content, Player owner)
        {
            if (content is IReplaceable and IPlayerOwned owned)
            {
                return owned.GetOwner() == owner;
            }
            return false;
        }
    }
}