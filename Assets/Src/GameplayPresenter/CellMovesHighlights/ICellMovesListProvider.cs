using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.CellMovesHighlights
{
    public interface ICellMovesListProvider
    {
        List<CellMove> GetCellMovesList(int playerId);
    }
}