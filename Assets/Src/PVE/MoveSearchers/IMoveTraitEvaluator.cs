using castledice_game_logic.MovesLogic;

namespace Src.PVE
{
    public interface IMoveTraitEvaluator
    {
        float EvaluateTrait(AbstractMove move);
    }
}