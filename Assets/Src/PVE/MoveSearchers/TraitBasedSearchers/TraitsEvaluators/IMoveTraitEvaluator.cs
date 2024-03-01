using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators
{
    public interface IMoveTraitEvaluator
    {
        float EvaluateTrait(AbstractMove move);
    }
}