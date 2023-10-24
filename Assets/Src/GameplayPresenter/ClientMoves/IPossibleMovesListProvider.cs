using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.ClientMoves
{
    public interface IPossibleMovesListProvider
    {
        List<AbstractMove> GetPossibleMoves(Vector2Int position, int playerId);
    }
}