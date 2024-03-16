using castledice_game_logic;
using castledice_game_logic.GameObjects;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.PVE.Checkers
{
    public class PlayerBaseChecker : IPlayerBaseChecker
    {
        public bool IsPlayerBase(Content content, Player owner)
        {
            if (content is CastleGO castle)
            {
                return castle.GetOwner() == owner;
            }
            return false;
        }
    }
}