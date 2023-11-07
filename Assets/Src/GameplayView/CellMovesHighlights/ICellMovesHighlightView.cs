using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayView.CellMovesHighlights
{
    public interface ICellMovesHighlightView
    {
        void HighlightCellMoves(List<CellMove> cellMoves);
    }
}