using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.GameWrappers
{
    public interface ITotalPossibleMovesProvider
    {
        List<AbstractMove> GetTotalPossibleMoves(int playerId);
    }
}