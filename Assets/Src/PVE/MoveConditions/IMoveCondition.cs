using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveConditions
{
    public interface IMoveCondition
    {
        bool IsSatisfiedBy(AbstractMove move);
    }
}