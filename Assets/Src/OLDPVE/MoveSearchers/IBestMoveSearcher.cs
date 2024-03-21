using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers
{
    public interface IBestMoveSearcher
    {
        AbstractMove GetBestMove(List<AbstractMove> moves);
    }
}