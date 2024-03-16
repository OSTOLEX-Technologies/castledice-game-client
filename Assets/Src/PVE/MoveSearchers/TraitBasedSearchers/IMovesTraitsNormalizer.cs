using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers
{
    public interface IMovesTraitsNormalizer
    {
        Dictionary<AbstractMove, MoveTraitsValues> NormalizeTraits(Dictionary<AbstractMove, MoveTraitsValues> movesToTraits);
    }
}