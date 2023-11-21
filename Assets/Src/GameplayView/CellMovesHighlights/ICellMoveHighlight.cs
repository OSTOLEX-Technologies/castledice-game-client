using castledice_game_logic.MovesLogic;

namespace Src.GameplayView.CellMovesHighlights
{
    public interface ICellMoveHighlight
    {
        void ShowHighlight(MoveType moveType);
        void HideHighlight(MoveType moveType);
        void HideAllHighlights();
    }
}