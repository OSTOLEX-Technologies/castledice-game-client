using System.Collections.Generic;
using castledice_game_logic.Math;
using Src.GameplayView.Grid;

namespace Src.GameplayView.CellMovesHighlights
{
    public class CellMovesHighlightsPlacer : ICellHighlightsPlacer
    {
        private readonly IGrid _grid;
        private readonly IUnityCellMoveHighlightsFactory _factory;

        public CellMovesHighlightsPlacer(IGrid grid, IUnityCellMoveHighlightsFactory factory)
        {
            _grid = grid;
            _factory = factory;
        }

        public Dictionary<Vector2Int, ICellMoveHighlight> PlaceHighlights()
        {
            var dictionary = new Dictionary<Vector2Int, ICellMoveHighlight>();
            foreach (var cell in _grid)
            {
                var position = cell.Position;
                var highlight = _factory.GetCellMoveHighlight();
                cell.AddChild(highlight.gameObject);
                dictionary.Add(position, highlight);
            }

            return dictionary;
        }
    }
}