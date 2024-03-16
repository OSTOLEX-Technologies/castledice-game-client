using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveSearchers
{
    public interface IBestMoveSearcher
    {
        AbstractMove GetBestMove(int playerId);
    }
}