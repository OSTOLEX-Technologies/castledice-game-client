using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers
{
    public interface IMoveTraitEvaluator
    {
        float EvaluateTrait(AbstractMove move);
    }
}