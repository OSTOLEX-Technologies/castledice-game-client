using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.GameplayView.Grid;
using Src.GameplayView.Highlights;
using Src.GameplayView.PlayerObjectsColor;

namespace Src.GameplayView.PlacedUnitsHighlights
{
    public class PlacedUnitsHighlightsView : IPlacedUnitsHighlightsView
    {
        private readonly IPlayerObjectsColorProvider _colorProvider;
        private readonly Dictionary<Vector2Int, ColoredHighlight> _highlights = new();

        public PlacedUnitsHighlightsView(IGrid grid, IColoredHighlightCreator coloredHighlightCreator, IPlayerObjectsColorProvider colorProvider)
        {
            _colorProvider = colorProvider;
            foreach (var cell in grid)
            {
                var highlight = coloredHighlightCreator.GetHighlight();
                var position = cell.Position;
                cell.AddChild(highlight.gameObject);
                highlight.Hide();
                _highlights.Add(position, highlight);
            }
        }

        public void ShowHighlight(Vector2Int position, Player player)
        {
            if (!_highlights.TryGetValue(position, out var highlight))
            {
                throw new InvalidOperationException("No underline for this position " + position);
            }
            highlight.Show();
            highlight.SetColor(_colorProvider.GetColor(player));
        }

        public void HideHighlight(Vector2Int position)
        {
            if (!_highlights.TryGetValue(position, out var highlight))
            {
                throw new InvalidOperationException("No underline for this position " + position);
            }
            highlight.Hide();
        }
    }
}