using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayView.CellMovesHighlights
{
    public class CellMovesHighlightView : ICellMovesHighlightView
    {
        private readonly Dictionary<Vector2Int, ICellMoveHighlight> _cellHighlights;
        
        public CellMovesHighlightView(Dictionary<Vector2Int, ICellMoveHighlight> cellHighlights)
        {
            _cellHighlights = cellHighlights;
        }
        
        public void HighlightCellMoves(List<CellMove> cellMoves)
        {
            foreach (var cellMove in cellMoves)
            {
                if (!_cellHighlights.ContainsKey(cellMove.Cell.Position))
                {
                    throw new System.ArgumentException("Cell move has unknown position: " + cellMove.Cell.Position);
                }
                _cellHighlights[cellMove.Cell.Position].ShowHighlight(cellMove.MoveType);
            }
        }

        public void HideHighlights()
        {
            foreach (var cellHighlight in _cellHighlights)
            {
                cellHighlight.Value.HideAllHighlights();
            }
        }
    }
}