using castledice_game_logic.MovesLogic;

namespace Src.General.MoveConditions
{
    public interface IMoveCondition
    {
        bool IsSatisfiedBy(AbstractMove move);
    }
}