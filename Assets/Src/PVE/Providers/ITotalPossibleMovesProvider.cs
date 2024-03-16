using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Providers
{
    public interface ITotalPossibleMovesProvider
    {
        List<AbstractMove> GetTotalPossibleMoves(int playerId);
    }
}