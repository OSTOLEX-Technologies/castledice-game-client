using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayView.CellMovesHighlights
{
    public class CellMovesHighlightView : ICellMovesHighlightView
    {
        private readonly Dictionary<Vector2Int, ICellHighlight> _cellHighlights;
        private readonly ICellHighlightsPlacer _cellHighlightsPlacer;
        
        public CellMovesHighlightView(ICellHighlightsPlacer cellHighlightsPlacer)
        {
            _cellHighlightsPlacer = cellHighlightsPlacer;
            _cellHighlights = _cellHighlightsPlacer.PlaceHighlights();
        }
        
        public void HighlightCellMoves(List<CellMove> cellMoves)
        {
            throw new System.NotImplementedException();
        }

        public void HideHighlights()
        {
            throw new System.NotImplementedException();
        }
    }
}