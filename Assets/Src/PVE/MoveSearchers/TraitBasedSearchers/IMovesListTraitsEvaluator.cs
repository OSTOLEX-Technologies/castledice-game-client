using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers
{
    public interface IMovesListTraitsEvaluator
    {
        Dictionary<AbstractMove, MoveTraitsValues> EvaluateTraitsForMoves(List<AbstractMove> moves);
    }
}